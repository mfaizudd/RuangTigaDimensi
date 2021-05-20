using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Geometry : MonoBehaviour
{
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject modelFrame;
    [SerializeField] private Transform[] vertices;
    [SerializeField] private Transform[] edges;
    [SerializeField] private Transform[] faces;
    [FormerlySerializedAs("vertexText")] [SerializeField] private PointText pointText;
    [SerializeField] private Transform worldCanvas;
    [SerializeField] private bool instantiateCanvas;
    [SerializeField, Min(2)] private int maxSelection = 2;

    private readonly Dictionary<string, List<PointText>> _points = new Dictionary<string, List<PointText>>();
    private readonly List<GeometryPoint> _indices = new List<GeometryPoint>(2);
    private const string Vertex = "Vertex";
    private const string Edge = "Edge";
    private const string Face = "Face";
    private LineRenderer _line;

    private void Awake()
    {
        TryGetComponent(out _line);
        _points.Add(Vertex, InitializePoints(vertices, Vertex));
        _points.Add(Edge, InitializePoints(edges, Edge));
        _points.Add(Face, InitializePoints(faces, Face));
        SetPointsVisibility(false, Edge, Face);
    }

    private void Update()
    {
        if (_line.positionCount <= 0) return;

        for (var i = 0; i < _line.positionCount; i++)
        {
            var index = _indices[i];
            var position = _points[index.Type][index.Index].FollowTarget.position;
            var direction = position.normalized;
            _line.SetPosition(i, position + direction * 0.1f);
        }
    }

    private List<PointText> InitializePoints(IReadOnlyCollection<Transform> points, string type)
    {
        var pointInstances = new List<PointText>(points.Count);
        foreach (var vertex in points)
        {
            var canvasInstance = instantiateCanvas ? Instantiate(worldCanvas, Vector3.zero, Quaternion.identity) : worldCanvas;
            var textInstance = Instantiate(pointText, canvasInstance);
            var textTransform = textInstance.transform;
            textTransform.position = vertex.position;
            textTransform.rotation = Quaternion.identity;
            textInstance.SetText(vertex.name);
            textInstance.FollowTarget = vertex;
            textInstance.Type = type;
            textInstance.PointClick += OnPointClick;
            pointInstances.Add(textInstance);
        }
        return pointInstances;
    }

    public void SetPointsVisibility(bool value, params string[] types)
    {
        foreach (var type in types)
        {
            if (!_points.TryGetValue(type, out var points))
            {
                Debug.Log($"Unsupported point type: {type}", this);
                continue;
            }
            
            foreach (var point in points)
            {
                point.gameObject.SetActive(value);
            }
        }
    }

    private void OnPointClick(PointText point, string type)
    {
        if (!_points.TryGetValue(type, out var points))
            throw new ArgumentException("Unsupported type", nameof(type));
        
        var index = points.IndexOf(point);
        var geometryPoint = new GeometryPoint {Index = index, Type = type};
        var existingIndex = _indices.IndexOf(geometryPoint);
        if (existingIndex >= 0)
        {
            _indices.RemoveAt(existingIndex);
            _line.positionCount = _indices.Count;
            return;
        }

        if (_line.positionCount >= maxSelection)
        {
            _indices.RemoveAt(0);
        }

        _indices.Add(geometryPoint);
        _line.positionCount = _indices.Count;
    }

    public void SetFrameVisibility(bool frameVisible)
    {
        model.SetActive(!frameVisible);
        modelFrame.SetActive(frameVisible);
    }

    public void SetRotation(Quaternion rotation)
    {
        StartCoroutine(RotateTo(rotation));
    }

    public void ResetRotation()
    {
        StartCoroutine(RotateTo(Quaternion.identity));
    }

    private IEnumerator RotateTo(Quaternion target)
    {
        const float transitionTime = 1f;
        var t = 0f;
        while (t < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, target, t);
            t += Time.deltaTime / transitionTime;
            yield return null;
        }
    }
}