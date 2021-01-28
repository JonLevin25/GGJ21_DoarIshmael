using UnityEngine;

namespace Utils.Events
{
    public class AnimationEventBridge : MonoBehaviour
    {
        [SerializeField] private UnityIntEvent callbacks;
        
        public void AnimationEvent(int param)
        {
            callbacks.Invoke(param);
        }
    }
}