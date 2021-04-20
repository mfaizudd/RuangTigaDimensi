using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ModelController : MonoBehaviour, MainControl.IModelActions
{
    [SerializeField] private Transform model = null;
    
    private MainControl _control;
    private Vector2 dragDelta = Vector2.zero;
    private bool isDragging = false;
    
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
        if (!isDragging) return;
        
        model.Rotate(Vector3.down, dragDelta.x, Space.Self);
        model.Rotate(Vector3.right, dragDelta.y, Space.World);
    }

    public void OnDragDelta(InputAction.CallbackContext context)
    {
        dragDelta = context.ReadValue<Vector2>();
    }

    public void OnDrag(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isDragging = true;
        }
        else if (context.canceled)
        {
            isDragging = false;
        }
    }
}
