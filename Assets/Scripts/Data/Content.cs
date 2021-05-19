using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class Content : IContent
    {
        [SerializeField] private string key;
        [SerializeField] private string name;
        [SerializeField] private string text;
        
        public string Key { get => key; set => key = value; }
        public string Name { get => name; set => name = value; }
        public string Text { get => text; set => text = value; }
    }
}