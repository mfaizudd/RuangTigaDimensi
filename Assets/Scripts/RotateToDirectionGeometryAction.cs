using System;
using UnityEngine;

public class RotateToDirectionGeometryAction : GeometryAction
{
    [SerializeField] private Vector3 direction;
    
    public override void Invoke(Geometry geometry)
    {
        var backDirection = new Vector3(direction.x, direction.y, direction.z * -1);
        var rotation = Quaternion.LookRotation(backDirection);
        geometry.SetRotation(rotation);
    }
}