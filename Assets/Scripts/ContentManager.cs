using System.Collections;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class ContentManager : MonoBehaviour
{
    [SerializeField] private Geometry geometry;
    [SerializeField] private GameObject[] userInterfaces;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CinemachineVirtualCamera mainVirtualCamera;
    [SerializeField] private Camera contentCamera;
    [SerializeField] private Transform contentContainer;
    [SerializeField] private Button closeButton;
    [SerializeField] private GeometryDimensionMenu dimensionMenu;

    private bool _zoomedIn = true;
    private ContentStage _currentStage;

    private void Awake()
    {
        geometry.LineCreated += OnLineCreated;
        geometry.LineDestroyed += OnLineDestroyed;
        closeButton.onClick.AddListener(OnClose);
    }

    private void OnClose()
    {
        geometry.ClearSelection();
    }

    private void OnLineDestroyed(bool cleanupOnly = false)
    {
        foreach (var ui in userInterfaces)
        {
            ui.SetActive(true);
        }

        if (!_zoomedIn && !cleanupOnly)
            StartCoroutine(CloseContent(0.5f));

        if (_currentStage == null) return;
        _currentStage.Cleanup();
        Destroy(_currentStage.gameObject);
    }

    private void OnLineCreated(Vector3[] linePoints, GeometryPoint[] points)
    {
        var sortedPoints = points.Select(p => p.Name).OrderBy(p => p);
        var key = string.Join("-", sortedPoints);
        var content = geometry.Contents.FirstOrDefault(c => c.Key == key);
        if (content == null || content.ContentPrefab == null)
        {
            OnLineDestroyed();
            return;
        }
        
        foreach (var ui in userInterfaces)
        {
            ui.SetActive(false);
        }

        if (_zoomedIn)
            StartCoroutine(OpenContent(0.5f));

        _currentStage = Instantiate(content.ContentPrefab, contentContainer.position, Quaternion.identity);
        _currentStage.transform.SetParent(contentContainer);
        _currentStage.Inject(geometry);
    }

    private IEnumerator OpenContent(float transitionTime)
    {
        var rect = mainCamera.rect;
        StartCoroutine(TweenUtil.AnimateFloat(rect.width, 0.5f, transitionTime, v =>
        {
            rect.width = v;
            mainCamera.rect = rect;
        }));
        var transposer = mainVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        var startOffset = transposer.m_FollowOffset;
        var endOffset = startOffset + Vector3.back * 3 + Vector3.up * 2;
        yield return StartCoroutine(TweenUtil.AnimateVector(startOffset, endOffset, transitionTime, v => transposer.m_FollowOffset = v));
        contentCamera.gameObject.SetActive(true);
        contentContainer.gameObject.SetActive(true);
        dimensionMenu.gameObject.SetActive(true);
        dimensionMenu.LoadInputValues();
        _zoomedIn = false;
    }

    private IEnumerator CloseContent(float transitionTime)
    {
        contentContainer.gameObject.SetActive(false);
        contentCamera.gameObject.SetActive(false);
        dimensionMenu.gameObject.SetActive(false);
        var rect = mainCamera.rect;
        StartCoroutine(TweenUtil.AnimateFloat(rect.width, 1f, transitionTime, v =>
        {
            rect.width = v;
            mainCamera.rect = rect;
        }));
        var transposer = mainVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        var startOffset = transposer.m_FollowOffset;
        var endOffset = startOffset + Vector3.forward * 3 + Vector3.down * 2;
        yield return StartCoroutine(TweenUtil.AnimateVector(startOffset, endOffset, transitionTime, v => transposer.m_FollowOffset = v));
        _zoomedIn = true;
    }
}