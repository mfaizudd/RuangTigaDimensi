using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStage : MonoBehaviour
{
    [SerializeField] private GameObject actionsHolder;
    
    private GeometryAction[] _actions;

    private void Awake()
    {
        _actions = actionsHolder.GetComponentsInChildren<GeometryAction>();
    }

    public void Inject(Geometry geometry)
    {
        foreach (var action in _actions)
        {
            action.Invoke(geometry);
        }
    }
}
