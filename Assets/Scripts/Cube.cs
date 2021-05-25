using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Cube : Geometry
{
    [SerializeField] private double side = 4;

    private void Start()
    {
        SetLength(side);
    }
    
    /// <summary>
    /// The length applied to cube will also affect the width and height
    /// </summary>
    /// <param name="length"></param>
    public override void SetLength(double length)
    {
        base.SetLength(length);
        Width = length;
        Height = length;
    }

    /// <summary>
    /// The height applied to cube will also affect the length and width
    /// </summary>
    /// <param name="height"></param>
    public override void SetHeight(double height)
    {
        base.SetHeight(height);
        Length = height;
        Width = height;
    }
    
    /// <summary>
    /// The width applied to cube will also affect length and height
    /// </summary>
    /// <param name="width"></param>
    public override void SetWidth(double width)
    {
        base.SetWidth(width);
        Length = width;
        Height = width;
    }
}