using UnityEngine;

namespace Utils.EditorUtils
{
    [RequireComponent(typeof(AudioSource))]
    public class DisableAudioSourceOnEditor : MonoBehaviour
    {
        [SerializeField] private bool enableSound = false;
        
        #if UNITY_EDITOR
        private void Awake()
        {
            if (enableSound)
            {
                return;
            }
            foreach (var audioSource in GetComponents<AudioSource>())
            {
                audioSource.Stop();
            }
        }
        #endif
    }
}