using System;
using UnityEngine;

public class ContentManager : MonoBehaviour
{
    [SerializeField] private Geometry geometry;
    [SerializeField] private GameObject[] userInterfaces;
    
    

    private void Awake()
    {
        geometry.LineCreated += OnLineCreated;
        geometry.LineDestroyed += OnLineDestroyed;
    }

    private void OnLineDestroyed()
    {
        foreach (var ui in userInterfaces)
        {
            ui.SetActive(true);
        }
    }

    private void OnLineCreated(Vector3[] linePoints)
    {
        foreach (var ui in userInterfaces)
        {
            ui.SetActive(false);
        }
    }
}