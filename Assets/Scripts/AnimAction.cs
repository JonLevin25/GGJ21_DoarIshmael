using System;
using System.Linq;
using UnityEngine;

[Serializable]
public struct AnimAction
{
    [SerializeField] private float ApproxTime;
    [SerializeField] private string[] Triggers;
    [Space]
    [SerializeField] private string IntPropName;
    [SerializeField] private int[] IntPropValues;

    public float Invoke(Animator anim)
    {
        Debug.Log($"Activating animation on ({anim.name}). {(Triggers.Any() ? Triggers.JoinStr() : "")} {IntPropName}=Rand[{IntPropValues.JoinStr()}]");
        foreach (var trigger in Triggers)
        {
            anim.SetTrigger(trigger);
        }

        if (!string.IsNullOrWhiteSpace(IntPropName))
        {
            var val = IntPropValues.RandomItem();
            anim.SetInteger(IntPropName, val);
        }

        return ApproxTime;
    }
}