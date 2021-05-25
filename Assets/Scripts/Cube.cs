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
        Width = length;
        Height = length;
        base.SetLength(length);
    }

    /// <summary>
    /// The height applied to cube will also affect the length and width
    /// </summary>
    /// <param name="height"></param>
    public override void SetHeight(double height)
    {
        Length = height;
        Width = height;
        base.SetHeight(height);
    }
    
    /// <summary>
    /// The width applied to cube will also affect length and height
    /// </summary>
    /// <param name="width"></param>
    public override void SetWidth(double width)
    {
        Length = width;
        Height = width;
        base.SetWidth(width);
    }
}