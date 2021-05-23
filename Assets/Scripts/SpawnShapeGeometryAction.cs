using UnityEngine;

public class SpawnShapeGeometryAction : GeometryAction
{
    [SerializeField] private GameObject shapePrefab;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Vector3 position;
    [SerializeField] private float gap = 0.5f;

    private GameObject _instantiatedShape;
    public override void Invoke(Geometry geometry)
    {
        var eulerRotation = Quaternion.Euler(rotation);
        var direction = position.normalized;
        var positionWithGap = position + direction * gap;
        var instance = Instantiate(shapePrefab, geometry.transform);
        instance.transform.localPosition = positionWithGap;
        instance.transform.localRotation = eulerRotation;
        _instantiatedShape = instance;
    }

    public override void Cleanup()
    {
        Destroy(_instantiatedShape);
    }
}