using UnityEngine;
public class RotateGeometryAction : GeometryAction
{
    [SerializeField] private Vector3 rotation;
    public override void Invoke(Geometry geometry)
    {
        var quaternionRotation = Quaternion.Euler(rotation);
        geometry.SetRotation(quaternionRotation);
    }
}