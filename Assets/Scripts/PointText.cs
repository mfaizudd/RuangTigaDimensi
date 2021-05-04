using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointText : MonoBehaviour
{
    public string Type { get; set; }
    public event Action<PointText, string> PointClick;
    
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private float gap = 1f;
    [SerializeField] private Button vertexButton;

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
        vertexButton.onClick.AddListener(OnVertexClick);
    }

    private void OnVertexClick()
    {
        PointClick?.Invoke(this, Type);
    }

    private void Update()
    {
        if (_isFollowTargetNull)
            return;

        var targetPosition = FollowTarget.position;
        var direction = (targetPosition - Vector3.zero).normalized;
        transform.position = targetPosition + direction * gap;

    }

    public void SetText(string text)
    {
        textMesh.text = text;
    }
}