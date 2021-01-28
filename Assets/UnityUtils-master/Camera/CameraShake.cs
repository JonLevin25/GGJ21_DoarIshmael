using System;
using UnityEngine;

namespace Utils.Camera
{
    public class CameraShake : MonoBehaviour
    {
    
        [SerializeField] private CameraShakeProfile cameraShakeProfile;
    
        private Vector3 _startPosition;
        private Quaternion _startRotation;
        private Vector3 _startScale;

        private float _trauma;
    
        private CameraShakeProfile _currentProfile;
    
    
        void Start()
        {
            var transformLocalCopy = transform;
            _startPosition = transformLocalCopy.localPosition;
            _startRotation = transformLocalCopy.localRotation;
            _startScale = transformLocalCopy.localScale;
        
        }


        [ContextMenu("Shake")]
        private void ShakeContextMenu()
        {
            Shake();
        }

        public void Shake(CameraShakeProfile profile = null)
        {
            if (profile == null)
            {
                profile = cameraShakeProfile;
            }

            if (profile == null)
            {
                Debug.Log("Profile is null");
                return;
            }

            _currentProfile = profile;
            _trauma = 1;
        }

        float GetNoise(float seed, float speed)
        {
            return Mathf.PerlinNoise(seed, Time.time * speed) * 2 - 1;
        }
    

        void Update()
        {
            if (_currentProfile == null)
            {
                return;
            }
        
            var locationOffset = new Vector3(
                GetNoise(0, _currentProfile.shakeSpeed) * _currentProfile.xMovement,
                GetNoise(1, _currentProfile.shakeSpeed) * _currentProfile.yMovement,
                0
            );
            locationOffset *= _trauma;
            var myTransform = transform;
            myTransform.position = _startPosition + locationOffset;

            var zRotationOffset = GetNoise(10, _currentProfile.shakeSpeed) * _currentProfile.zRotation;
            zRotationOffset *= (float)Math.Pow(_trauma, 2);
            myTransform.localRotation = Quaternion.Euler(_startRotation.eulerAngles.x, _startRotation.eulerAngles.y, zRotationOffset);
        
            _trauma -= (Time.deltaTime / _currentProfile.duration);
            if (_trauma <= 0)
            {
                _currentProfile = null;
                myTransform.position = _startPosition;
                myTransform.localRotation = _startRotation;
            }
        }
    }
}
