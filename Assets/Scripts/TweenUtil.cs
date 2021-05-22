using System;
using System.Collections;
using UnityEngine;

public static class TweenUtil
{
    public static IEnumerator AnimateFloat(float start, float end, float transitionTime, Action<float> callback)
    {
        var t = 0f;
        while (t < 1)
        {
            callback(Mathf.Lerp(start, end, Mathf.SmoothStep(0, 1, t)));
            t += Time.deltaTime / transitionTime;
            yield return null;
        }
    }

    public static IEnumerator AnimateVector(Vector3 start, Vector3 end, float transitionTime, Action<Vector3> callback)
    {
        var t = 0f;
        while (t < 1)
        {
            callback(Vector3.Lerp(start, end, Mathf.SmoothStep(0, 1, t)));
            t += Time.deltaTime / transitionTime;
            yield return null;
        }
    }
    
    public static IEnumerator AnimateQuaternion(Quaternion start, Quaternion end, float transitionTime, Action<Quaternion> callback)
        {
            var t = 0f;
            while (t < 1)
            {
                callback(Quaternion.Slerp(start, end, Mathf.SmoothStep(0, 1, t)));
                t += Time.deltaTime / transitionTime;
                yield return null;
            }
        }
}