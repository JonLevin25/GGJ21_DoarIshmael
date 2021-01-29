using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEditor;
using UnityEngine;

public class FixSkinned : MonoBehaviour
{
    [SerializeField] private GameObject rootPrefab;
    [NaughtyAttributes.Button("Fix")]
    public void Fix()
    {
        var skinned = GetComponentsInChildren<SkinnedMeshRenderer>();
        var root = GetRoot(skinned[0]);
        var paths = GetBonePaths(skinned[0], root);
        
        var rootParent = root.parent;
        DestroyImmediate(root.gameObject);
        var newRootPrefab = (GameObject) PrefabUtility.InstantiatePrefab(rootPrefab, rootParent);
        var newRoot = newRootPrefab.transform;
        var newBones = GetBonesFromPaths(paths, newRoot).Skip(1);

        foreach (var skin in skinned)
        {
            skin.bones = newBones.Prepend(newRoot).ToArray();
            skin.rootBone = newRoot;
        }
    }

    private Transform GetRoot(SkinnedMeshRenderer skin) => skin.rootBone;

    private int[][] GetBonePaths(SkinnedMeshRenderer skin, Transform root)
    {
        return skin.bones.Select(bone => GetScenePath(bone, root)).ToArray();
    }

    // TODO
    private Transform[] GetBonesFromPaths(int[][] boneScenePaths, Transform root)
    {
        return boneScenePaths.Select(bonePath => FindTransByPath(bonePath, root)).ToArray();
    }

    private Transform FindTransByPath(int[] path, Transform root)
    {
        var trans = root;
        foreach (var sibIdx in path)
        {
            trans = trans.GetChild(sibIdx);
        }

        return trans;
    }

    private int[] GetScenePath(Transform trans, Transform root)
    {
        var parentStack = new Stack<int>();
        var curPar = trans;
        while (curPar != root)
        {
            parentStack.Push(curPar.GetSiblingIndex());
            curPar = curPar.parent;
        }

        return parentStack.ToArray();
    }
}
