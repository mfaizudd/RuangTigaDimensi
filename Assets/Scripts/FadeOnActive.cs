using System;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadeOnActive : MonoBehaviour
{
    private CanvasGroup _group;

    private void Awake()
    {
        TryGetComponent(out _group);
    }

    private void OnEnable()
    {
        if (_group is null) return;

        StartCoroutine(TweenUtil.AnimateFloat(0f, 1, 0.5f, x => _group.alpha = x));
    }
}