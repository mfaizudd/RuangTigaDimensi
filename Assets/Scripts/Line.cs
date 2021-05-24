using System;
using UnityEngine;

public class Line : MonoBehaviour
{
    public Vector3 LocalPointA
    {
        get => localPointA;
        set
        {
            localPointA = value;
            _lineRenderer.SetPosition(0, localPointA);
        }
    }

    public Vector3 LocalPointB
    {
        get => localPointB;
        set
        {
            localPointB = value;
            _lineRenderer.SetPosition(1, localPointB);
        }
    }

    [SerializeField] private Vector3 localPointA;
    [SerializeField] private Vector3 localPointB;

    private LineRenderer _lineRenderer;

    private void Awake()
    {
        if (!TryGetComponent(out _lineRenderer)) return;
        
        _lineRenderer.SetPosition(0, localPointA);
        _lineRenderer.SetPosition(1, localPointB);
    }
}