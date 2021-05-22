#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

namespace EnsignBandeng.UI.Editor
{
    [CustomEditor(typeof(EToggle))]
    public class EToggleEditor : ToggleEditor
    {
        private SerializedProperty _text;
        private SerializedProperty _alternateActiveText;
        private SerializedProperty _alternateText;
        private SerializedProperty _swapToPressedSprite;
        private SerializedProperty _textMesh;
        private SerializedProperty _fontSize;
        private SerializedProperty _renameObject;
        private SerializedProperty _data;
        private SerializedProperty _transition;

        protected override void OnEnable()
        {
            base.OnEnable();
            _text = serializedObject.FindProperty("text");
            _alternateActiveText = serializedObject.FindProperty("alternateActiveText");
            _alternateText = serializedObject.FindProperty("alternateText");
            _swapToPressedSprite = serializedObject.FindProperty("swapToPressedSprite");
            _textMesh = serializedObject.FindProperty("textMesh");
            _fontSize = serializedObject.FindProperty("fontSize");
            _renameObject = serializedObject.FindProperty("renameObject");
            _data = serializedObject.FindProperty("data");
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

            EditorGUILayout.PropertyField(_swapToPressedSprite);
            if (_swapToPressedSprite.boolValue)
            {
                if (target is EToggle toggle) toggle.transition = Selectable.Transition.SpriteSwap;
            }
            EditorGUILayout.PropertyField(_textMesh);
            EditorGUILayout.PropertyField(_fontSize);
            EditorGUILayout.PropertyField(_renameObject);
            EditorGUILayout.PropertyField(_data);
            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();
        }
    }
}
#endif
