using UnityEngine;

public class StrayPlatform : MonoBehaviour
{
    [SerializeField]
    float _completeSnapDistance = 30f;
    [SerializeField]
    float _noSnapDistance = 100f;
    [SerializeField]
    float _platformSize = 10f;
    [SerializeField]
    Transform _mergeTargetTransform;
    float _distanceFromTarget;


    // Use late update here to make sure the rotation of the Island controller can do it's thing before this platform tries to match.
    // It's really important to have thigns synced nicely.
    void LateUpdate()
    {
        _distanceFromTarget = (gameObject.transform.position - _mergeTargetTransform.position).magnitude;
        float lerpFactor = -1f / (_noSnapDistance - _completeSnapDistance) * (_distanceFromTarget - _completeSnapDistance) + 1f;
        Debug.Log(lerpFactor);
        UpdateRotation(lerpFactor);
        UpdatePosition(lerpFactor);
    }

    void UpdateRotation(float lerpFactor)
    {
        float currentY = transform.eulerAngles.y;
        float targetY = _mergeTargetTransform.eulerAngles.y;
        //////if ((targetY - currentY) % 90 == 0)
        //////    return;

        float closestMultiple = 90 * Mathf.Round((currentY - targetY) / 90) % 360;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, targetY + closestMultiple, 0), lerpFactor);
    }

    void UpdatePosition(float lerpFactor)
    {
        float closestX= _platformSize * Mathf.Round(transform.position.x / _platformSize);
        float closestZ = _platformSize * Mathf.Round(transform.position.z / _platformSize);
        transform.position = Vector3.Lerp(transform.position, new Vector3(closestX, 0, closestZ), lerpFactor);
    }
}
