using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EnsignBandeng.UI
{
    
    public class EButton : Button
    {
        public string Text
        {
            get => text;
            set
            {
                text = value;
                if (textMesh == null) return;
                textMesh.text = value;
            }
        }
        
        [SerializeField] private string text;
        [SerializeField] private TextMeshProUGUI textMesh;
        [SerializeField] private float fontSize = 22;
        [SerializeField] private bool renameObject = false;
        
        #if UNITY_EDITOR
        protected override void OnValidate()
        {
            if (renameObject)
            {
                gameObject.name = $"{text} Button";
            }
            if (textMesh == null) return;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
        }

        protected override void Reset()
        {
            base.Reset();
            textMesh = GetComponentInChildren<TextMeshProUGUI>();
        }
        #endif
    }
}