using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [Serializable]
    public class Content
    {
        [SerializeField] private string key;
        [SerializeField] private ContentStage contentPrefab;
        
        public string Key { get => key; set => key = value; }
        public ContentStage ContentPrefab { get => contentPrefab; set => contentPrefab = value; }
    }
}