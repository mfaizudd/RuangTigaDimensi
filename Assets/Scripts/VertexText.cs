using System;
using TMPro;
using UnityEngine;

public class VertexText : MonoBehaviour
{
    private bool _isFollowTargetNull;
    private TextMeshPro _textMesh;
    private Transform _followTarget;

    public Transform FollowTarget
    {
        get => _followTarget;
        set
        {
            _isFollowTargetNull = value == null;
            _followTarget = value;
        }
    }

    private void Awake()
    {
        _isFollowTargetNull = FollowTarget == null;
        TryGetComponent(out _textMesh);
    }

    private void Update()
    {
        if (_isFollowTargetNull)
            return;

        transform.position = FollowTarget.position;

    }

    public void SetText(string text)
    {
        _textMesh.text = text;
    }
}