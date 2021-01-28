using UnityEngine;

namespace Utils.EditorUtils
{
    public class OnlyOnEditor : MonoBehaviour
    {
        #if !UNITY_EDITOR
        void Awake()
        {
            DestroyImmediate(this);
        }
        #endif
    }
}