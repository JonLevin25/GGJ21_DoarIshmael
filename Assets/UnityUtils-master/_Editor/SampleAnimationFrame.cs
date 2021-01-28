using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.EditorUtils
{
    [ExecuteInEditMode]
    public class SampleAnimationFrame : MonoBehaviour
    {
        #if UNITY_EDITOR

        [Range(0, 60)]
        [SerializeField] private int _frame;
        [SerializeField] private List<Animator> _animators;
        

        private void OnValidate()
        {
            //Debug.Log(_frame);
            foreach (var animator in _animators)
            {
                var clipInfo = animator.GetCurrentAnimatorClipInfo(0)[0];
                var clip = clipInfo.clip;
                var time = _frame / clip.frameRate;
                clip.SampleAnimation(animator.gameObject, time);
            }
        }
        
        #endif
    }
}