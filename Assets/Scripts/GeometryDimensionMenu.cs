using System;
using System.Globalization;
using EnsignBandeng.UI;
using TMPro;
using UnityEngine;

public class GeometryDimensionMenu : MonoBehaviour
{
    [SerializeField] private Geometry geometry;
    [SerializeField] private EButton applyButton;
    [SerializeField] private TMP_InputField lengthInput, widthInput, heightInput;

    private void Awake()
    {
        applyButton.onClick.AddListener(OnApplyClicked);
        if (geometry is null)
        {
            Debug.LogError("No geometry set", this);
            return;
        }

        LoadInputValues();
    }

    private void OnEnable()
    {
        LoadInputValues();
    }

    public void LoadInputValues()
    {
        lengthInput.text = geometry.Length.ToString(CultureInfo.CurrentCulture);
        widthInput.text = geometry.Width.ToString(CultureInfo.CurrentCulture);
        heightInput.text = geometry.Height.ToString(CultureInfo.CurrentCulture);
    }

    private void OnApplyClicked()
    {
        if (!double.TryParse(lengthInput.text, out var length)) return;
        if (!double.TryParse(widthInput.text, out var width)) return;
        if (!double.TryParse(heightInput.text, out var height)) return;

        var originalLength = geometry.Length;
        var originalWidth = geometry.Width;
        var originalHeight = geometry.Height;
        // Only update if changed
        if (Math.Abs(originalLength - length) > 0)
            geometry.SetLength(Math.Max(length, 1));
        if (Math.Abs(originalWidth - width) > 0)
            geometry.SetWidth(Math.Max(width, 1));
        if (Math.Abs(originalHeight - height) > 0)
            geometry.SetHeight(Math.Max(height, 1));
        LoadInputValues();
    }
}