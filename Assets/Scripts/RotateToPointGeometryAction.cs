using System;
using System.Linq;
using UnityEngine;

public class RotateToPointGeometryAction : GeometryAction
{
    [SerializeField] private PointType pointType;
    [SerializeField] private string pointName;

    private void Awake()
    {
        if (pointName != "") return;
        Debug.LogError("No point name specified");
    }

    public override void Invoke(Geometry geometry)
    {
        var point = pointType switch
        {
            PointType.Vertex => geometry.Vertices.FirstOrDefault(v => v.name == pointName),
            PointType.Edge => geometry.Edges.FirstOrDefault(e => e.name == pointName),
            PointType.Face => geometry.Faces.FirstOrDefault(f => f.name == pointName),
            _ => throw new ArgumentOutOfRangeException()
        };
        if (point is null) return;

        var direction = point.localPosition.normalized;
        var backDirection = new Vector3(direction.x, direction.y, direction.z * -1);
        Debug.Log(backDirection);
        var rotation = Quaternion.LookRotation(backDirection);
        geometry.SetRotation(rotation);
    }

    private enum PointType
    {
        Vertex,
        Edge,
        Face
    }
}