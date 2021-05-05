using System;

namespace Data
{
    [Serializable]
    public class Content : IContent
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
    }
}