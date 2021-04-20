using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Geometry : MonoBehaviour
{
    [SerializeField] private Transform[] vertices;
    [SerializeField] private TextMeshPro vertexText;

    private void Awake()
    {
        foreach (var vertex in vertices)
        {
            var textInstance = Instantiate(vertexText, vertex.position, Quaternion.identity);
            textInstance.text = vertex.name;
        }
    }
}