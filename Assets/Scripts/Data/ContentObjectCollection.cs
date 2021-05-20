using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "New Content Collection", menuName = "Content/Content Object Collection", order = 0)]
    public class ContentObjectCollection : ScriptableObject
    {
        [SerializeField] private List<ContentObject> contents;

        public void ShowContent(string key, Vector3 location, Geometry geometry)
        {
            var content = contents.Find(c => c.Key == key);
            if (content == null) return;

            var instance = Instantiate(content.ContentPrefab, location, Quaternion.identity);
            instance.Inject(geometry);
        }
    }
}