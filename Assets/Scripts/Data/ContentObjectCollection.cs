using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "New Content Collection", menuName = "Content/Content Object Collection", order = 0)]
    public class ContentObjectCollection : ScriptableObject
    {
        [SerializeField] private List<ContentObject> content;

        public List<ContentObject> Content => content;
    }
}