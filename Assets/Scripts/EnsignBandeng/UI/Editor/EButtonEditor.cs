#if UNITY_EDITOR
using EnsignBandeng.Editor;
using UnityEditor;
using UnityEditor.UI;

namespace EnsignBandeng.UI.Editor
{
    [CustomEditor(typeof(EButton))]
    public class EButtonEditor : ButtonEditor
    {
        private SerializedProperty _text;
        private SerializedProperty _textMesh;
        private SerializedProperty _fontSize;
        private SerializedProperty _renameObject;

        protected override void OnEnable()
        {
            base.OnEnable();
            _text = serializedObject.FindProperty("text");
            _textMesh = serializedObject.FindProperty("textMesh");
            _fontSize = serializedObject.FindProperty("fontSize");
            _renameObject = serializedObject.FindProperty("renameObject");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_text);
            EditorGUILayout.PropertyField(_textMesh);
            EditorGUILayout.PropertyField(_fontSize);
            EditorGUILayout.PropertyField(_renameObject);
            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();
        }
    }
}
#endif