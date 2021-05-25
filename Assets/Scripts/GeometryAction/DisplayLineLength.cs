using System;
using TMPro;
using UnityEngine;

public class DisplayLineLength : GeometryAction
{
    [SerializeField] private LengthType type;
    [SerializeField] private float scale = 1;
    [SerializeField] private string unit = "cm";
    private TextMeshProUGUI _textMesh;

    public override void Invoke(Geometry geometry)
    {
        var length = type switch
        {
            LengthType.Length => geometry.Length,
            LengthType.Width => geometry.Width,
            LengthType.Height => geometry.Height,
            _ => throw new ArgumentOutOfRangeException()
        };
        geometry.OnLengthChanged += OnLengthChanged;
        geometry.OnWidthChanged += OnLengthChanged;
        geometry.OnHeightChanged += OnLengthChanged;
        length *= scale;

        _textMesh = GetComponentInChildren<TextMeshProUGUI>();
        OnLengthChanged(length);
    }

    private void OnLengthChanged(double length)
    {
        _textMesh.text = $"Panjang garis: {length} {unit}";
    }

    private enum LengthType
    {
        Length,
        Width,
        Height
    }
}