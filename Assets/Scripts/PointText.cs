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
        vertexToggle.image.color = pressed ? Color.black : Color.white;
        vertexToggle.TextMesh.color = pressed ? Color.white : Color.black;
        PointClick?.Invoke(this, Type, pressed);
    }

    public void ToggleWithoutNotify(bool value)
    {
        vertexToggle.image.color = value ? Color.black : Color.white;
        vertexToggle.TextMesh.color = value ? Color.white : Color.black;
        vertexToggle.ToggleWithoutNotify(value);
    }

    public void Toggle(bool value)
    {
        vertexToggle.isOn = value;
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