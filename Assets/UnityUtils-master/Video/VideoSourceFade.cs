using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

namespace Utils.Video
{
    public class VideoSourceFade : MonoBehaviour
    {
        private float fadeTimeSeconds = 0.1f;

        [SerializeField] private GameObject[] disableWhenStartFading;

        async void Start()
        {
            var vp = GetComponent<VideoPlayer>();
            await Task.Delay(TimeSpan.FromSeconds(vp.length - fadeTimeSeconds));
            foreach (var go in disableWhenStartFading)
            {
                go.SetActive(false);
            }
            for (float f = 0; f < 1.06f; f += 0.05f)
            {
                vp.targetCameraAlpha = Mathf.Max(1 - f, 0);
                await Task.Delay(1);
            }
            gameObject.SetActive(false);

        }

    }
}
