using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [CreateAssetMenu(fileName = "New Content Object", menuName = "Content/New Content", order = 0)]
    public class ContentObject : ScriptableObject, IContent
    {
        [SerializeField] private string key;
        [SerializeField] private string contentName;
        [SerializeField] private string text;
        
        public string Key { get => key; set => key = value; }
        public string Name { get => contentName; set => contentName = value; }
        public string Text { get => text; set => text = value; }
    }
}