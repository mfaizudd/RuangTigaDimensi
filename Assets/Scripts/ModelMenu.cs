using EnsignBandeng.UI;
using UnityEngine;
using UnityEngine.UI;

public class ModelMenu : MonoBehaviour
{
    [Header("UI Space")] 
    [SerializeField] private EToggle wireframeToggle;
    [SerializeField] private EToggle perspectiveToggle;
    [SerializeField] private EButton resetRotationButton;
    [SerializeField] private EToggleGroup visibilityGroup;

    [Header("World Space")] 
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Geometry geometry;

    private void Awake()
    {
        wireframeToggle.onValueChanged.AddListener(OnWireframeToggled);
        perspectiveToggle.onValueChanged.AddListener(OnPerspectiveToggled);
        resetRotationButton.onClick.AddListener(OnResetRotationClicked);
        visibilityGroup.ToggleValueChanged += OnToggleValueChanged;
    }

    private void OnToggleValueChanged(bool value, Toggle toggle)
    {
        if (!(toggle is EToggle eToggle))
            return;

        geometry.SetPointsVisibility(eToggle.Data, value);
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