using TMPro;
using UnityEngine;

public class VertexText : MonoBehaviour
{
    [SerializeField] private float gap = 1f;
    
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

        var targetPosition = FollowTarget.position;
        var direction = (targetPosition - Vector3.zero).normalized;
        transform.position = targetPosition + direction * gap;

    }

    public void SetText(string text)
    {
        _textMesh.text = text;
    }
}