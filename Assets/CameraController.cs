using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera _virtaulCamera;

    [SerializeField]
    float _zoomRate = 0.001f;
    [SerializeField]
    float _maxZoomFactor = 3;
    [SerializeField]
    float _minZoomFactor = 0.5f;

    float _currentZoomFactor = 1;
    Vector3 _initialOffet;
    CinemachineTransposer _transposer;

    private void Awake()
    {
        _transposer = _virtaulCamera.GetCinemachineComponent<CinemachineTransposer>();
        _initialOffet = _transposer.m_FollowOffset;
    }

    // Update is called once per frame
    void Update()
    {
        _transposer.m_FollowOffset = _initialOffet * _currentZoomFactor;
    }

    public void OnScrollWheelY(InputValue value)
    {
        float signOfValue = value.Get<float>();
        if (signOfValue == 0)
        {
            return;
        }
        float newZoomFactor = _currentZoomFactor - Mathf.Sign(value.Get<float>()) * _zoomRate;
        _currentZoomFactor = Mathf.Clamp(newZoomFactor, _minZoomFactor, _maxZoomFactor);
    }
}
