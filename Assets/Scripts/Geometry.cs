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

    private void Awake()
    {
        _vertexInstances = new List<VertexText>(vertices.Length);
        foreach (var vertex in vertices)
        {
            var canvasInstance = instantiateCanvas ? Instantiate(worldCanvas, Vector3.zero, Quaternion.identity) : worldCanvas;
            var textInstance = Instantiate(vertexText, canvasInstance);
            var textTransform = textInstance.transform;
            textTransform.position = vertex.position;
            textTransform.rotation = Quaternion.identity;
            textInstance.SetText(vertex.name);
            textInstance.FollowTarget = vertex;
            textInstance.VertexClick += OnVertexClick;
            _vertexInstances.Add(textInstance);
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