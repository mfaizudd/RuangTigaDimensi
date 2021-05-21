using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "New Content Collection", menuName = "Content/Content Collection", order = 0)]
    public class ContentCollection : ScriptableObject, IList<Content>
    {
        [SerializeField] private List<Content> contents;

        public IEnumerator<Content> GetEnumerator()
        {
            return contents.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Content item)
        {
            contents.Add(item);
        }

        public void Clear()
        {
            contents.Clear();
        }

        public bool Contains(Content item)
        {
            return contents.Contains(item);
        }

        public void CopyTo(Content[] array, int arrayIndex)
        {
            contents.CopyTo(array, arrayIndex);
        }

        public bool Remove(Content item)
        {
            return contents.Remove(item);
        }

        public int Count => contents.Count;
        public bool IsReadOnly => true;
        public int IndexOf(Content item)
        {
            return contents.IndexOf(item);
        }

        public void Insert(int index, Content item)
        {
            contents.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            contents.RemoveAt(index);
        }

        public Content this[int index]
        {
            get => contents[index];
            set => contents[index] = value;
        }
    }
}