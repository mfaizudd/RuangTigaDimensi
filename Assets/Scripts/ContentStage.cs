using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContentStage : MonoBehaviour
{
    private GeometryAction[] _actions;
    private TextMeshProUGUI _textMesh;
    private Image _image;

    private void Awake()
    {
        _actions = GetComponentsInChildren<GeometryAction>();
        _textMesh = GetComponentInChildren<TextMeshProUGUI>();
        if (_textMesh is null)
        {
            var textMeshObject = new GameObject("Text");
            textMeshObject.transform.SetParent(transform);
            textMeshObject.transform.localScale = Vector3.one;
            _textMesh = textMeshObject.AddComponent<TextMeshProUGUI>();
        }
        _textMesh.color = Color.black;
        _textMesh.horizontalAlignment = HorizontalAlignmentOptions.Center;
        _textMesh.verticalAlignment = VerticalAlignmentOptions.Middle;
        _image = GetComponentInChildren<Image>();
        if (_image is null)
        {
            var imageObject = new GameObject("Image");
            imageObject.transform.SetParent(transform);
            imageObject.transform.localScale = Vector3.one;
            _image = imageObject.AddComponent<Image>();
            imageObject.SetActive(false);
        }
        _image.preserveAspect = true;
    }

    private void Start()
    {
        transform.localScale = Vector3.one;
        if (transform is RectTransform rect) rect.sizeDelta = new Vector2(-20, -20);
    }

    public void Inject(Geometry geometry)
    {
        transform.SetAsFirstSibling();
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
