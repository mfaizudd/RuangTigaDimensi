using System;
using UnityEngine;
using UnityEngine.UI;

namespace EnsignBandeng.UI
{
    public class EToggleGroup : ToggleGroup
    {
        public event Action<bool, Toggle> ToggleValueChanged;
        
        protected override void Awake()
        {
            base.Awake();
            foreach (var toggle in m_Toggles)
            {
                toggle.onValueChanged.AddListener(value => ToggleValueChanged?.Invoke(value, toggle));
            }
        }
    }
}