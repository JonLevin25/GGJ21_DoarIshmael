using UnityEngine;

namespace Utils.Events
{
    public class LifetimeCallbacks : MonoBehaviour
    {
        [SerializeField] private UnityEventAction callback;

        private void OnEnable()
        {
            callback.Invoke();
        }
    }
}