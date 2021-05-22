using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ContentManager : MonoBehaviour
{
    [SerializeField] private Geometry geometry;
    [SerializeField] private GameObject[] userInterfaces;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CinemachineVirtualCamera mainVirtualCamera;
    [SerializeField] private Camera contentCamera;

    private bool _zoomedIn = true;

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

        if (!_zoomedIn)
            StartCoroutine(CloseContent(0.5f));
    }

    private void OnLineCreated(Vector3[] linePoints)
    {
        foreach (var ui in userInterfaces)
        {
            ui.SetActive(false);
        }

        if (_zoomedIn)
            StartCoroutine(OpenContent(0.5f));
    }

    private IEnumerator OpenContent(float transitionTime)
    {
        var rect = mainCamera.rect;
        StartCoroutine(TweenUtil.AnimateFloat(rect.width, 0.3f, transitionTime, v =>
        {
            rect.width = v;
            mainCamera.rect = rect;
        }));
        var transposer = mainVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        var startOffset = transposer.m_FollowOffset;
        var endOffset = startOffset + Vector3.back * 5;
        yield return StartCoroutine(TweenUtil.AnimateVector(startOffset, endOffset, transitionTime, v => transposer.m_FollowOffset = v));
        contentCamera.gameObject.SetActive(true);
        _zoomedIn = false;
    }

    private IEnumerator CloseContent(float transitionTime)
    {
        contentCamera.gameObject.SetActive(false);
        var rect = mainCamera.rect;
        StartCoroutine(TweenUtil.AnimateFloat(rect.width, 1f, transitionTime, v =>
        {
            rect.width = v;
            mainCamera.rect = rect;
        }));
        var transposer = mainVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        var startOffset = transposer.m_FollowOffset;
        var endOffset = startOffset + Vector3.forward * 5;
        yield return StartCoroutine(TweenUtil.AnimateVector(startOffset, endOffset, transitionTime, v => transposer.m_FollowOffset = v));
        _zoomedIn = true;
    }
}