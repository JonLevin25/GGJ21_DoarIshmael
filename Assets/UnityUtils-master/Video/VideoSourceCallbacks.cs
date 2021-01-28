using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;
using Utils.Events;

namespace Utils.Video
{
    public class VideoSourceCallbacks : MonoBehaviour
    {
        [SerializeField] private UnityEventAction onStarted;
        [SerializeField] private UnityEventAction onEnded;
         
        void Start()
        {
            onStarted?.Invoke();
            var vp = GetComponent<VideoPlayer>();
            vp.loopPointReached += source => onEnded.Invoke();
        }
    }
}