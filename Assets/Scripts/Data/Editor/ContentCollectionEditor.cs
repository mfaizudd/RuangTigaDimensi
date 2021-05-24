#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using EnsignBandeng.Editor;
using UnityEditor;
using UnityEngine;

namespace Data.Editor
{
    [CustomEditor(typeof(ContentCollection))]
    public class ContentCollectionEditor : ExtendedEditor<ContentCollectionEditor>
    {
        private SerializedProperty _geometry;
        private SerializedProperty _contents;
        private Object _prefabToClone;
        private void OnEnable()
        {
            _geometry = GetProperty("geometry");
            _contents = GetProperty("contents");
        }

        public override void OnInspectorGUI()
        {
            DrawProperty(_geometry);
            if (_geometry.objectReferenceValue is { })
                DrawButton("Generate", OnGenerate);
            DrawProperty(_contents);
            _prefabToClone = EditorGUILayout.ObjectField(_prefabToClone, typeof(GameObject), false);
            SaveChanges();
        }

        private void OnGenerate()
        {
            if (!(_geometry.objectReferenceValue is Geometry geometry)) return;
            _contents.arraySize = 0;
            GeneratePoint(geometry.Vertices);
            GeneratePoint(geometry.Vertices, geometry.Edges);
            GeneratePoint(geometry.Vertices, geometry.Faces);
        }

        private void GeneratePoint(IReadOnlyList<Transform> points)
        {
            var orderedPoints = points.OrderBy(x => x.name).ToArray();
            for (var i = 0; i < 1; i++)
            {
                var iVertex = orderedPoints[i].name;
                for (var j = i + 1; j < orderedPoints.Length; j++)
                {
                    var jVertex = orderedPoints[j].name;
                    var key = $"{iVertex}-{jVertex}";
                    var assetName = $"{iVertex}{jVertex}";
                    var asset = AssetDatabase.LoadAssetAtPath($"Assets/Prefabs/Contents/{_geometry.objectReferenceValue.name}/{assetName}.prefab", typeof(GameObject));
                    _contents.arraySize++;
                    var element = _contents.GetArrayElementAtIndex(_contents.arraySize - 1);
                    var elementKey = element.FindPropertyRelative("key");
                    var elementPrefab = element.FindPropertyRelative("contentPrefab");
                    elementKey.stringValue = key;
                    elementPrefab.objectReferenceValue = asset;
                }
            }
        }
        
        private void GeneratePoint(IEnumerable<Transform> points, IEnumerable<Transform> targetPoints)
        {
            foreach (var point in points.OrderBy(x=>x.name))
            {
                var iVertex = point.name;
                foreach (var targetPoint in targetPoints.OrderBy(x=>x.name))
                {
                    var jVertex = targetPoint.name;
                    var key = $"{iVertex}-{jVertex}";
                    var assetPath = $"Assets/Prefabs/Contents/{_geometry.objectReferenceValue.name}/{key}.prefab";
                    var asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject));
                    _contents.arraySize++;
                    var element = _contents.GetArrayElementAtIndex(_contents.arraySize - 1);
                    var elementKey = element.FindPropertyRelative("key");
                    var elementPrefab = element.FindPropertyRelative("contentPrefab");
                    elementKey.stringValue = key;
                    if (asset is null && _prefabToClone is { })
                    {
                        var prefabPath = AssetDatabase.GetAssetPath(_prefabToClone);
                        AssetDatabase.CopyAsset(prefabPath, assetPath);
                        asset = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
                    }
                    elementPrefab.objectReferenceValue = asset;
                }
                break;
            }
        }
    }
}
#endif