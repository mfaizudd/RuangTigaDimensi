#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace EnsignBandeng.Editor
{
    public class ExtendedEditor<T> : UnityEditor.Editor where T : UnityEngine.Object
    {
        protected T Target => target as T;
        private SerializedProperty GetProperty(string propertyName) => serializedObject.FindProperty(propertyName);

        protected void PrefixLabel(string label)
        {
            EditorGUILayout.PrefixLabel(label);
        }
        
        protected void DrawProperty(SerializedProperty property)
        {
            EditorGUILayout.PropertyField(property);
        }

        protected void DrawProperty(SerializedProperty property, string label)
        {
            EditorGUILayout.PropertyField(property, new GUIContent(label));
        }

        protected void DrawToggle(SerializedProperty property)
        {
            property.boolValue = EditorGUILayout.Toggle(property.boolValue);
        }

        protected void DrawIntField(SerializedProperty property)
        {
            property.intValue = EditorGUILayout.IntField(property.intValue);
        }
    
        protected void DrawProperty(string propertyName)
        {
            EditorGUILayout.PropertyField(GetProperty(propertyName));
        }

        protected void DrawProperty(string propertyName, string label)
        {
            EditorGUILayout.PropertyField(GetProperty(propertyName), new GUIContent(label));
        }

        protected void DrawToggle(string propertyName)
        {
            var prop = GetProperty(propertyName);
            prop.boolValue = EditorGUILayout.Toggle(prop.boolValue);
        }

        protected void DrawIntField(string propertyName)
        {
            var prop = GetProperty(propertyName);
            prop.intValue = EditorGUILayout.IntField(prop.intValue);
        }

        protected void DrawButton(string caption, Action callback)
        {
            if(GUILayout.Button(caption))
            {
                callback?.Invoke();
            }
        }
    }
}
#endif