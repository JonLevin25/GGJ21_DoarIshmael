using System;
using UnityEngine;

namespace Utils.Camera
{
    public class CameraAspectRatio : MonoBehaviour
    {
        private UnityEngine.Camera _cam;
        [SerializeField] private Vector2 targetResolution; //the resolution the game is designed for

        void Awake(){

            _cam = GetComponent<UnityEngine.Camera>();

            float targetAspect = targetResolution.x / targetResolution.y;
            float currentAspect = Screen.width / (float)Screen.height;
            
            if (Math.Abs(currentAspect - targetAspect) > 0.01f){
                _cam.orthographicSize /= currentAspect / targetAspect;
            }
        }
    }
}