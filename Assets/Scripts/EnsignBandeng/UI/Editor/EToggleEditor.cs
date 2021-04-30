#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace EnsignBandeng.UI.Editor
{
    [CustomEditor(typeof(EToggle))]
    public class EToggleEditor : ToggleEditor
    {
        private SerializedProperty _text;
        private SerializedProperty _alternateActiveText;
        private SerializedProperty _alternateText;
        private SerializedProperty _textMesh;
        private SerializedProperty _fontSize;
        private SerializedProperty _renameObject;

        protected override void OnEnable()
        {
            base.OnEnable();
            _text = serializedObject.FindProperty("text");
            _alternateActiveText = serializedObject.FindProperty("alternateActiveText");
            _alternateText = serializedObject.FindProperty("alternateText");
            _textMesh = serializedObject.FindProperty("textMesh");
            _fontSize = serializedObject.FindProperty("fontSize");
            _renameObject = serializedObject.FindProperty("renameObject");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_text);
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(_alternateActiveText);
            if (_alternateActiveText.boolValue && GUILayout.Button("Switch value"))
            {
                var temp = _text.stringValue;
                _text.stringValue = _alternateText.stringValue;
                _alternateText.stringValue = temp;
            }
            EditorGUILayout.EndHorizontal();
            
            if (_alternateActiveText.boolValue)
                EditorGUILayout.PropertyField(_alternateText);
            
            EditorGUILayout.PropertyField(_textMesh);
            EditorGUILayout.PropertyField(_fontSize);
            EditorGUILayout.PropertyField(_renameObject);
            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();
        }
    }
}
#endif
