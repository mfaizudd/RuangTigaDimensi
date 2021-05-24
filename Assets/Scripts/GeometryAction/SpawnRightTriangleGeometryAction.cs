using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnRightTriangleGeometryAction : GeometryAction
{
    [SerializeField] private Line linePrefab;
    [SerializeField] private Triangle[] triangles;

    private readonly List<GameObject> _triangleObjects = new List<GameObject>();

    public override void Invoke(Geometry geometry)
    {
        foreach (var triangle in triangles)
        {
            var aVertex = geometry.Vertices.FirstOrDefault(x => x.name == triangle.A);
            var bVertex = geometry.Vertices.FirstOrDefault(x => x.name == triangle.B);
            var cVertex = geometry.Vertices.FirstOrDefault(x => x.name == triangle.C);
            if (aVertex is null)
                Debug.LogError("A vertex not found", this);
            if (bVertex is null)
                Debug.LogError("B vertex not found", this);
            if (cVertex is null)
                Debug.LogError("C vertex not found", this);
            if (aVertex is null || bVertex is null || cVertex is null)
                return;

            var triangleInstance = new GameObject("Triangle");
            _triangleObjects.Add(triangleInstance);
            triangleInstance.transform.SetParent(geometry.transform);
            var aPosition = aVertex.transform.position;
            var bPosition = bVertex.transform.position;
            var cPosition = cVertex.transform.position;
        
            var lineB = Instantiate(linePrefab, triangleInstance.transform);
            lineB.LocalPointA = cPosition + cPosition.normalized * 0.1f;
            lineB.LocalPointB = aPosition + aPosition.normalized * 0.1f;
        
            var lineA = Instantiate(linePrefab, triangleInstance.transform);
            lineA.LocalPointA = cPosition + cPosition.normalized * 0.1f;
            lineA.LocalPointB = bPosition + bPosition.normalized * 0.1f;

            var aDirection = (lineA.LocalPointB - lineA.LocalPointA).normalized;
            var bDirection = (lineB.LocalPointB - lineB.LocalPointA).normalized;
        
            var lineSquareA = Instantiate(linePrefab, triangleInstance.transform);
            lineSquareA.LocalPointA = lineA.LocalPointA + aDirection;
            lineSquareA.LocalPointB = lineSquareA.LocalPointA + bDirection;
        
            var lineSquareB = Instantiate(linePrefab, triangleInstance.transform);
            lineSquareB.LocalPointA = lineSquareA.LocalPointB;
            lineSquareB.LocalPointB = lineSquareB.LocalPointA + -aDirection;
        }
    }

    public override void Cleanup()
    {
        foreach (var triangle in _triangleObjects)
        {
            Destroy(triangle);
        }
    }
    
    [Serializable]
    private struct Triangle
    {
        public string A => aVertexName;
        public string B => bVertexName;
        public string C => cVertexName;
        [SerializeField] private string aVertexName;
        [SerializeField] private string bVertexName;
        [SerializeField] private string cVertexName;
    }
}