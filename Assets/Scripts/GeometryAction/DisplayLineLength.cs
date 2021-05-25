using System;
using TMPro;
using UnityEngine;

public class DisplayLineLength : GeometryAction
{
    [SerializeField] private LengthType type;
    [SerializeField] private float scale = 1;
    
    public override void Invoke(Geometry geometry)
    {
        var length = type switch
        {
            LengthType.Length => geometry.Length,
            LengthType.Width => geometry.Width,
            LengthType.Height => geometry.Height,
            _ => throw new ArgumentOutOfRangeException()
        };
        length *= scale;

        var textMesh = GetComponentInChildren<TextMeshProUGUI>();
        textMesh.text = $"Panjang garis: {length}";
    }

    private enum LengthType
    {
        Length,
        Width,
        Height
    }
}