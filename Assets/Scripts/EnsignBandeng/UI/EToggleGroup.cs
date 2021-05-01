using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace EnsignBandeng.UI
{
    public class EToggleGroup : ToggleGroup
    {
        public event Action<bool, Toggle> ToggleValueChanged;

        [SerializeField] private bool disableGroup;
        
        protected override void Start()
        {
            base.Start();
            var toggles = m_Toggles.ToList();
            foreach (var toggle in toggles)
            {
                toggle.onValueChanged.AddListener(value => ToggleValueChanged?.Invoke(value, toggle));
                
                if (!disableGroup) continue;
                UnregisterToggle(toggle);
                toggle.@group = null;
            }
        }
    }
}