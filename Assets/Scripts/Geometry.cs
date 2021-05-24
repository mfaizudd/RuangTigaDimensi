using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

public class Geometry : MonoBehaviour
{
    #region Events
    public delegate void OnLineCreated(Vector3[] linePoints, GeometryPoint[] points);
    public event OnLineCreated LineCreated;

    public delegate void OnLineDestroyed(bool cleanupOnly = false);
    public event OnLineDestroyed LineDestroyed;
    #endregion
    
    public ContentCollection Contents => contents;
    public IEnumerable<Transform> Vertices => vertices;
    public IEnumerable<Transform> Edges => edges;
    public IEnumerable<Transform> Faces => faces;
    public bool IsWireframeEnabled => modelFrame.activeSelf;
    
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject modelFrame;
    [SerializeField] private Transform[] vertices;
    [SerializeField] private Transform[] edges;
    [SerializeField] private Transform[] faces;
    [SerializeField] private PointText pointTextPrefab;
    [SerializeField] private Transform worldCanvas;
    [SerializeField] private bool instantiateCanvas;
    [SerializeField, Min(2)] private int maxSelection = 2;
    [SerializeField] private ContentCollection contents;

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
            var textInstance = Instantiate(pointTextPrefab, canvasInstance);
            var textTransform = textInstance.transform;
            textTransform.position = vertex.position;
            textTransform.rotation = Quaternion.identity;
            textInstance.Text = vertex.name;
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

    private void OnPointClick(PointText point, string type, bool pressed)
    {
        if (!_points.TryGetValue(type, out var points))
            throw new ArgumentException("Unsupported type", nameof(type));
        
        var index = points.IndexOf(point);
        var geometryPoint = new GeometryPoint {Name=point.Text, Index = index, Type = type};
        var existingIndex = _indices.IndexOf(geometryPoint);
        if (existingIndex >= 0 || !pressed)
        {
            _indices.RemoveAt(existingIndex);
            _line.positionCount = _indices.Count;
            LineDestroyed?.Invoke();
            return;
        }

        if (_line.positionCount >= maxSelection)
        {
            points[_indices[0].Index].ToggleWithoutNotify(false);
            _indices.RemoveAt(0);
            LineDestroyed?.Invoke(true);
        }

        _indices.Add(geometryPoint);
        _line.positionCount = _indices.Count;

        if (_line.positionCount != maxSelection) return;

        var linePoints = new Vector3[_line.positionCount];
        _line.GetPositions(linePoints);
        LineCreated?.Invoke(linePoints, _indices.ToArray());
    }

    public void ClearSelection()
    {
        var points = _indices.ToList();
        foreach (var pointText in points.Select(point => _points[point.Type][point.Index]))
        {
            pointText.Toggle(false);
        }
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
        yield return StartCoroutine(TweenUtil.AnimateQuaternion(transform.rotation, target, 0.5f,
            r => transform.rotation = r));
    }
}