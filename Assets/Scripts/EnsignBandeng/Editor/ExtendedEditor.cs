using System;
using UnityEditor;
using UnityEngine;

namespace EnsignBandeng.Editor
{
    public class ExtendedEditor<T> : UnityEditor.Editor where T : UnityEngine.Object
    {
        protected T Target => target as T;
        protected SerializedProperty GetProperty(string name) => serializedObject.FindProperty(name);

        protected void PrefixLabel(string label)
        {
            EditorGUILayout.PrefixLabel(label);
        }
    
        protected void DrawProperty(string name)
        {
            EditorGUILayout.PropertyField(GetProperty(name));
        }

        protected void DrawProperty(string name, string label)
        {
            EditorGUILayout.PropertyField(GetProperty(name), new GUIContent(label));
        }

        protected void DrawToggle(string name)
        {
            var prop = GetProperty(name);
            prop.boolValue = EditorGUILayout.Toggle(prop.boolValue);
        }

        protected void DrawIntField(string name)
        {
            var prop = GetProperty(name);
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