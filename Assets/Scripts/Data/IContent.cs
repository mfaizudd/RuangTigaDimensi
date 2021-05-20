using UnityEngine;

namespace Data
{
    public interface IContent
    {
        string Key { get; set; }
        string Name { get; set; }
        ContentStage ContentPrefab { get; set; }
    }
}