using System;
using System.Collections;
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
    private bool _reverse;

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

        var direction = _reverse ? -1 : 1;
        model.transform.Rotate(Vector3.down, _dragDelta.x * speed * direction, Space.Self);
        model.transform.Rotate(Vector3.right, _dragDelta.y * speed, Space.World);
    }

    private void FixedUpdate()
    {
        if (_isDragging) return;
        var t = transform;
        var hit = Physics.Raycast(t.position, model.transform.up, 100f, 1 << 6);
        _reverse = hit;
    }

    public void OnDragDelta(InputAction.CallbackContext context)
    {
        _dragDelta = context.ReadValue<Vector2>();
    }

    public void OnDrag(InputAction.CallbackContext context)
    {
        if (context.started)
        {
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
