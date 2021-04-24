using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Geometry : MonoBehaviour
{
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject modelFrame;
    [SerializeField] private Transform[] vertices;
    [SerializeField] private VertexText vertexText;
    

    private void Awake()
    {
        foreach (var vertex in vertices)
        {
            var textInstance = Instantiate(vertexText, vertex.position, Quaternion.identity);
            textInstance.SetText(vertex.name);
            textInstance.FollowTarget = vertex;
        }
    }

    public void SetFrameVisibility(bool frameVisible)
    {
        model.SetActive(!frameVisible);
        modelFrame.SetActive(frameVisible);
    }
}