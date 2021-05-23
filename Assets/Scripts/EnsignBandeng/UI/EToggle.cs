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
        
        public string Data
        {
            get => data;
            set => data = value;
        }
        
        [SerializeField] private string text;
        [SerializeField] private bool alternateActiveText = false;
        [SerializeField] private string alternateText;
        [SerializeField] private bool swapToPressedSprite = false;
        [SerializeField] private TextMeshProUGUI textMesh;
        [SerializeField] private float fontSize = 22;
        [SerializeField] private bool renameObject = false;
        [SerializeField] private string data;

        private string _defaultText;
        private Sprite _originalSprite;

        protected override void Awake()
        {
            base.Awake();
            _defaultText = text;
            _originalSprite = image.sprite;
            onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(bool value)
        {
            if (alternateActiveText)
                Text = value ? alternateText : _defaultText;

            if (swapToPressedSprite)
                image.sprite = value ? spriteState.pressedSprite : _originalSprite;
        }

        public void ToggleWithoutNotify(bool value)
        {
            SetIsOnWithoutNotify(value);
            OnValueChanged(value);
        }

        #if UNITY_EDITOR
        protected override void OnValidate()
        {
            if (renameObject)
            {
                gameObject.name = $"{text} Toggle";
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