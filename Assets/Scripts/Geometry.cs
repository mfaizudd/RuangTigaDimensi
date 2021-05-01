using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class Geometry : MonoBehaviour
{
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject modelFrame;
    [SerializeField] private Transform[] vertices;
    [SerializeField] private Transform[] edges;
    [SerializeField] private Transform[] faces;
    [SerializeField] private VertexText vertexText;
    [SerializeField] private Transform worldCanvas;
    [SerializeField] private bool instantiateCanvas;

    private List<VertexText> _vertexInstances;
    private List<VertexText> _edgeInstances;
    private List<VertexText> _faceInstances;

    private void Awake()
    {
        _vertexInstances = InitializePoints(vertices);
        _edgeInstances = InitializePoints(edges);
        _faceInstances = InitializePoints(faces);
        SetPointsVisibility("All", false);
    }

    private List<VertexText> InitializePoints(IReadOnlyCollection<Transform> points)
    {
        var pointInstances = new List<VertexText>(points.Count);
        foreach (var vertex in points)
        {
            var canvasInstance = instantiateCanvas ? Instantiate(worldCanvas, Vector3.zero, Quaternion.identity) : worldCanvas;
            var textInstance = Instantiate(vertexText, canvasInstance);
            var textTransform = textInstance.transform;
            textTransform.position = vertex.position;
            textTransform.rotation = Quaternion.identity;
            textInstance.SetText(vertex.name);
            textInstance.FollowTarget = vertex;
            textInstance.VertexClick += OnVertexClick;
            pointInstances.Add(textInstance);
        }
        return pointInstances;
    }

    public void SetPointsVisibility(string pointType, bool value)
    {
        switch (pointType)
        {
            case "Vertex":
                SetPointsVisibility(_vertexInstances, value);
                break;
            case "Edge":
                SetPointsVisibility(_edgeInstances, value);
                break;
            case "Face":
                SetPointsVisibility(_faceInstances, value);
                break;
            case "All":
                SetPointsVisibility(_vertexInstances, value);
                SetPointsVisibility(_edgeInstances, value);
                SetPointsVisibility(_faceInstances, value);
                break;
            default:
                Debug.Log($"Unsupported point type: {pointType}", this);
                break;
        }
    }

    public void SetPointsVisibility(IEnumerable<VertexText> points, bool value)
    {
        foreach (var point in points)
        {
            point.gameObject.SetActive(value);
        }
    }

    private void OnVertexClick(VertexText vertex)
    {
        var index = _vertexInstances.IndexOf(vertex);
        
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