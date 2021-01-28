using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

namespace Utils.UI
{
 
    [CanEditMultipleObjects, CustomEditor(typeof(NonDrawingGraphic), false)]
    public class NonDrawingGraphicEditor : GraphicEditor
    {
        public override void OnInspectorGUI ()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(base.m_Script, new GUILayoutOption[0]);
            // skipping AppearanceControlsGUI
            RaycastControlsGUI();
            serializedObject.ApplyModifiedProperties();
        }
    }
}