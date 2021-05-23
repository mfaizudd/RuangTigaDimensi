using System;
using EnsignBandeng.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PointText : MonoBehaviour
{
    public string Type { get; set; }
    public event Action<PointText, string, bool> PointClick;
    
    [SerializeField] private float gap = 1f;
    [SerializeField] private EToggle vertexToggle;

    private bool _isFollowTargetNull;
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
        vertexToggle.onValueChanged.AddListener(OnVertexClick);
    }

    private void OnVertexClick(bool pressed)
    {
        PointClick?.Invoke(this, Type, pressed);
    }

    public void Toggle(bool value)
    {
        vertexToggle.ToggleWithoutNotify(value);
    }

    private void Update()
    {
        if (_isFollowTargetNull)
            return;

        var targetPosition = FollowTarget.position;
        var direction = (targetPosition - Vector3.zero).normalized;
        transform.position = targetPosition + direction * gap;

    }

    public string Text
    {
        get => vertexToggle.Text;
        set => vertexToggle.Text = value;
    }
}