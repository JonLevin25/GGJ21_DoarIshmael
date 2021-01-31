using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FixSkinned : MonoBehaviour
{
#if UNITY_EDITOR

    [SerializeField] private GameObject rootPrefab;

    public void Reset()
    {
        Fix();
    }

    [NaughtyAttributes.Button("Fix incative using active)")]
    public void FixInactiveUsingActive()
    {
        var activeSkinned = GetComponentsInChildren<SkinnedMeshRenderer>()[0];
        var allSkinned = GetComponentsInChildren<SkinnedMeshRenderer>(includeInactive: true);
        
        foreach (var skin in allSkinned)
        {
            skin.bones = activeSkinned.bones;
            skin.rootBone = activeSkinned.rootBone;
        }
    }

    [NaughtyAttributes.Button("Fix")]
    public void Fix()
    {
        var skinned = GetComponentsInChildren<SkinnedMeshRenderer>();
        var root = GetRoot(skinned[0]);
        var paths = GetBonePaths(skinned[0], root);
        
        var rootParent = root.parent;
        DestroyImmediate(root.gameObject);
        var newRootPrefab = (GameObject) UnityEditor.PrefabUtility.InstantiatePrefab(rootPrefab, rootParent);
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
#endif
}
