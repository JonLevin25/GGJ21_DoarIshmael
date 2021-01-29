using System;
using UnityEngine;


public class PackageTarget : MonoBehaviour
{
    [Serializable]
    public struct ActivateAnim
    {
        public string IntPropName;
        public int[] Values;
    }
    
    [SerializeField] private Animator animator;
    [SerializeField] private ActivateAnim animProp;
    

    public virtual void OnPackageHit(PackageBox package)
    {
        ActivateRandom(animProp);
    }

    public void ActivateRandom(ActivateAnim animProp)
    {
        var value = animProp.Values.RandomItem();
        animator.SetInteger(animProp.IntPropName, value);
    }
}