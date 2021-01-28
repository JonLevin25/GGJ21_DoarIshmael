using UnityEngine;

namespace Utils.EditorUtils
{
    public class DisableOnEditor : MonoBehaviour
    {

        #if UNITY_EDITOR
        void Start()
        {
            gameObject.SetActive(false);
        }
        #endif
    }
}