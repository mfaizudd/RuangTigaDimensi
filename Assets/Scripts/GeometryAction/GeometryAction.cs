using UnityEngine;

public abstract class GeometryAction : MonoBehaviour
{
    public abstract void Invoke(Geometry geometry);

    public virtual void Cleanup() {}

}