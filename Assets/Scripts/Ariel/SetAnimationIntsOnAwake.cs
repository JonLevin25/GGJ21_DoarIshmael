using System;
using UnityEngine;
using Utils;

namespace Ariel
{
    public class SetAnimationIntsOnAwake : MonoBehaviour
    {
        [Serializable]
        class NameAndValues
        {
            public string name;
            public int[] values;
        }

        [SerializeField]
        private NameAndValues[] values;

        private void Awake()
        {
            var anim = GetComponent<Animator>();
            foreach (var value in values)
            {
                anim.SetInteger(value.name, value.values.Choice());
            }
        }
    }
}