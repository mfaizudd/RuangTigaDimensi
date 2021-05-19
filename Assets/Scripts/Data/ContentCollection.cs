using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "New Content Collection", menuName = "Content/Content Collection", order = 0)]
    public class ContentCollection : ScriptableObject
    {
        [SerializeField] private List<Content> content;

        public List<Content> Content => content;
    }
}