using System;
using UnityEngine;
using UnityEngine.UI;

public class ModelMenu : MonoBehaviour
{
    [Header("UI Space")] 
    [SerializeField] private Toggle wireframeToggle;
    [SerializeField] private Toggle perspectiveToggle;
    [SerializeField] private Button resetRotationButton;

    [Header("World Space")] 
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Geometry geometry;

    private void Awake()
    {
        wireframeToggle.onValueChanged.AddListener(OnWireframeToggled);
        perspectiveToggle.onValueChanged.AddListener(OnPerspectiveToggled);
        resetRotationButton.onClick.AddListener(OnResetRotationClicked);
    }

    private void OnWireframeToggled(bool value)
    {
        geometry.SetFrameVisibility(value);
    }

    private void OnPerspectiveToggled(bool value)
    {
        mainCamera.orthographic = value;
    }

    private void OnResetRotationClicked()
    {
        geometry.ResetRotation();
    }
}