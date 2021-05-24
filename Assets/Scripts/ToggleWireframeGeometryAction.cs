using UnityEngine;

public class ToggleWireframeGeometryAction : GeometryAction
{
    [SerializeField] private bool isOn;

    private bool _originalState;
    private Geometry _geometry;
    public override void Invoke(Geometry geometry)
    {
        _originalState = geometry.IsWireframeEnabled;
        geometry.SetFrameVisibility(isOn);
        _geometry = geometry;
    }

    public override void Cleanup()
    {
        _geometry.SetFrameVisibility(_originalState);
    }
}