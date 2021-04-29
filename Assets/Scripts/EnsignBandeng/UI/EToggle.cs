using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EnsignBandeng.UI
{
    public class EToggle : Toggle
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
        [SerializeField] private bool alternateActiveText = false;
        [SerializeField] private string alternateText;
        [SerializeField] private TextMeshProUGUI textMesh;
        [SerializeField] private float fontSize = 22;
        [SerializeField] private bool renameObject = false;

        private string _defaultText;

        protected override void Awake()
        {
            base.Awake();
            _defaultText = text;
            if (alternateActiveText)
                onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(bool value)
        {
            Text = value ? alternateText : _defaultText;
        }

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
    }
}