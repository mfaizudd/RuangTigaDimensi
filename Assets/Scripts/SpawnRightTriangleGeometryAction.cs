using System;
using System.Linq;
using UnityEngine;

public class SpawnRightTriangleGeometryAction : GeometryAction
{
    [SerializeField] private Line linePrefab;
    [Header("Vertices")]
    [SerializeField]  private string aVertexName;
    [SerializeField]  private string bVertexName;
    [SerializeField]  private string cVertexName;

    private GameObject _triangle;
    private Transform _aVertex;
    private Transform _bVertex;
    private Transform _cVertex;

    public override void Invoke(Geometry geometry)
    {
        _aVertex = geometry.Vertices.FirstOrDefault(x => x.name == aVertexName);
        _bVertex = geometry.Vertices.FirstOrDefault(x => x.name == bVertexName);
        _cVertex = geometry.Vertices.FirstOrDefault(x => x.name == cVertexName);
        if (_aVertex is null)
            Debug.LogError("A vertex not found", this);
        if (_bVertex is null)
            Debug.LogError("B vertex not found", this);
        if (_cVertex is null)
            Debug.LogError("C vertex not found", this);
        if (_aVertex is null || _bVertex is null || _cVertex is null)
            return;

        _triangle = new GameObject("Triangle");
        _triangle.transform.SetParent(geometry.transform);
        var aPosition = _aVertex.transform.position;
        var bPosition = _bVertex.transform.position;
        var cPosition = _cVertex.transform.position;
        
        var lineB = Instantiate(linePrefab, _triangle.transform);
        lineB.LocalPointA = cPosition + cPosition.normalized * 0.1f;
        lineB.LocalPointB = aPosition + aPosition.normalized * 0.1f;
        
        var lineA = Instantiate(linePrefab, _triangle.transform);
        lineA.LocalPointA = cPosition + cPosition.normalized * 0.1f;
        lineA.LocalPointB = bPosition + bPosition.normalized * 0.1f;

        var aDirection = (lineA.LocalPointB - lineA.LocalPointA).normalized;
        var bDirection = (lineB.LocalPointB - lineB.LocalPointA).normalized;
        
        var lineSquareA = Instantiate(linePrefab, _triangle.transform);
        lineSquareA.LocalPointA = lineA.LocalPointA + aDirection;
        lineSquareA.LocalPointB = lineSquareA.LocalPointA + bDirection;
        
        var lineSquareB = Instantiate(linePrefab, _triangle.transform);
        lineSquareB.LocalPointA = lineSquareA.LocalPointB;
        lineSquareB.LocalPointB = lineSquareB.LocalPointA + -aDirection;
    }

    public override void Cleanup()
    {
        Destroy(_triangle.gameObject);
    }
}