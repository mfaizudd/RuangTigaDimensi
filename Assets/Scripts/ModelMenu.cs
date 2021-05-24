using EnsignBandeng.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
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

    [Header("Game Menu")] 
    [SerializeField] private EButton exitButton;
    [SerializeField] private GameObject exitMenu;

    private void Awake()
    {
        wireframeToggle.onValueChanged.AddListener(OnWireframeToggled);
        perspectiveToggle.onValueChanged.AddListener(OnPerspectiveToggled);
        resetRotationButton.onClick.AddListener(OnResetRotationClicked);
        exitButton.onClick.AddListener(OnExitClicked);
        visibilityGroup.ToggleValueChanged += OnToggleValueChanged;
    }

    private void OnExitClicked()
    {
        exitMenu.SetActive(true);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    private void OnToggleValueChanged(bool value, Toggle toggle)
    {
        if (!(toggle is EToggle eToggle))
            return;

        geometry.SetPointsVisibility(value, eToggle.Data);
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