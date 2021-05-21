using System;
using Cinemachine;
using UnityEngine;

public class ContentManager : MonoBehaviour
{
    [SerializeField] private Geometry geometry;
    [SerializeField] private GameObject[] userInterfaces;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CinemachineVirtualCamera mainVirtualCamera;
    [SerializeField] private Camera contentCamera;

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

        var rect = mainCamera.rect;
        rect.width = 0.3f;
        mainCamera.rect = rect;
        var transposer = mainVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        if (transposer == null)
        {
            Debug.LogError("No transposer", this);
            return;
        }

        transposer.m_FollowOffset += Vector3.back * 5;
        contentCamera.gameObject.SetActive(true);
    }
}