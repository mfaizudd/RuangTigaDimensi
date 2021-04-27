using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EnsignBandeng.UI
{
    
    [RequireComponent(typeof(Button))]
    public class ButtonText : MonoBehaviour
    {
        [SerializeField] private string text;
        [SerializeField] private TextMeshProUGUI textMesh;
        [SerializeField] private float fontSize = 22;
        [SerializeField] private bool renameObject = true;

        protected void Reset()
        {
            textMesh = GetComponentInChildren<TextMeshProUGUI>();
            if (textMesh == null) return;
            fontSize = textMesh.fontSize;
        }

        protected void OnValidate()
        {
            if (renameObject)
            {
                gameObject.name = $"{text} Button";
            }
            if (textMesh == null) return;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
        }
    }
}
