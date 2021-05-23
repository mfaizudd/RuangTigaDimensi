using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStage : MonoBehaviour
{
    private GeometryAction[] _actions;

    private void Awake()
    {
        _actions = GetComponentsInChildren<GeometryAction>();
    }

    public void Inject(Geometry geometry)
    {
        foreach (var action in _actions)
        {
            action.Invoke(geometry);
        }
    }

    public void Cleanup()
    {
        foreach (var action in _actions)
        {
            action.Cleanup();
        }
    }
}
