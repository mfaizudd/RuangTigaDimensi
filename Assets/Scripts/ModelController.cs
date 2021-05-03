using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ModelController : MonoBehaviour, MainControl.IModelActions
{
    [SerializeField] private Geometry model;
    [SerializeField] private float speed = 1f;
    
    private MainControl _control;
    private Vector2 _dragDelta = Vector2.zero;
    private Vector2 _point = Vector2.zero;
    private bool _isDragging;

    private void Awake()
    {
        _control = new MainControl();
        _control.Model.SetCallbacks(this);
    }

    private void OnEnable()
    {
        _control.Enable();
    }

    private void Update()
    {
        if (!_isDragging) return;
        
        model.StopAllCoroutines();
        model.transform.Rotate(Vector3.down, _dragDelta.x * speed, Space.Self);
        model.transform.Rotate(Vector3.right, _dragDelta.y * speed, Space.World);
    }

    public void OnDragDelta(InputAction.CallbackContext context)
    {
        _dragDelta = context.ReadValue<Vector2>();
    }

    public void OnDrag(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            var pointer = new PointerEventData(EventSystem.current)
            {
                position = _point
            };
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, results);
            if (results.Count > 0) return;
            
            _isDragging = true;
        }
        else if (context.canceled)
        {
            _isDragging = false;
        }
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        _point = context.ReadValue<Vector2>();
    }
}
