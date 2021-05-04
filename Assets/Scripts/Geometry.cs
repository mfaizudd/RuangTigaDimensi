using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        _points.Add(Edge, InitializePoints(vertices, Edge));
        _points.Add(Face, InitializePoints(vertices, Face));
        SetPointsVisibility(false, Edge, Face);
    }

    private void Update()
    {
        if (_line.positionCount <= 0) return;

        _line.SetPositions(_indices.Select(i => _points[i.Type][i.Index].transform.position).ToArray());
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
        if (!_points.TryGetValue(type, out var points)) return;
        
        var index = points.IndexOf(point);
        _indices.Add(new GeometryPoint{Index = index, Type = type});
        _line.positionCount = _indices.Count;
    }

    public void SetFrameVisibility(bool frameVisible)
    {
        model.SetActive(!frameVisible);
        modelFrame.SetActive(frameVisible);
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