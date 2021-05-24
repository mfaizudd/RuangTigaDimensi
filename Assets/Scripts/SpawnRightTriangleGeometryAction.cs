using System;
using System.Linq;
using UnityEngine;

public class SpawnRightTriangleGeometryAction : GeometryAction
{
    [SerializeField] private Material material;
    [Header("Vertices")]
    [SerializeField]  private string aVertexName;
    [SerializeField]  private string bVertexName;
    [SerializeField]  private string cVertexName;

    private LineRenderer _lineRenderer;
    private Transform[] _vertices;
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

        _vertices = new[] {_cVertex, _aVertex, _bVertex, _cVertex};
        var instance = new GameObject();
        _lineRenderer = instance.AddComponent<LineRenderer>();
        _lineRenderer.positionCount = 7;
        _lineRenderer.material = material;
        _lineRenderer.startWidth = 0.1f;
        _lineRenderer.endWidth = 0.1f;
        _lineRenderer.SetPositions(_vertices.Select(x => x.position).ToArray());
    }

    private void Update()
    {
        if (_lineRenderer is null || _lineRenderer.positionCount <= 0) return;
        
        _lineRenderer.SetPositions(_vertices.Select(x=>x.position).ToArray());
        var positionCount = _lineRenderer.positionCount;
        var cPosition = _cVertex.position;
        var aDirection = (_aVertex.position - cPosition).normalized;
        var bDirection = (_bVertex.position - cPosition).normalized;
        _lineRenderer.SetPosition(positionCount - 3, cPosition + aDirection);
        _lineRenderer.SetPosition(positionCount - 2, cPosition + aDirection + bDirection);
        _lineRenderer.SetPosition(positionCount - 1, cPosition + bDirection);
    }

    public override void Cleanup()
    {
        Destroy(_lineRenderer.gameObject);
    }
}