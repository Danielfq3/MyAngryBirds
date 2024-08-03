using Cinemachine;
using Cinemachine.Utility;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float minZoom = 4;
    [SerializeField]
    private float maxZoom = 12;
    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera;

    [SerializeField]
    private GameObject _lookPoint;

    private Vector3 dragOrigin;
    private Vector3 originalPos;

    [SerializeField]
    private float cameraZoomSensitivity = 1;

    private Vector3 _origin;
    private Vector3 _difference;

    private Camera _mainCamera;

    private bool _isDragging;

    private void Awake() => _mainCamera = Camera.main;

    public void OnDrag(InputAction.CallbackContext ctx)
    {
        if (ctx.started) _origin = GetMousePosition;
        _isDragging = ctx.performed;
    }

    private void LateUpdate()
    {
        if (!_isDragging) return;
        _difference = GetMousePosition - transform.position;
        transform.position = _origin - _difference;
    }

    private Vector3 GetMousePosition => _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

    void Zoom(float increment)
    {
        _virtualCamera.m_Lens.OrthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, minZoom, maxZoom);
    }

    private void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPreviousPosition = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePreviousPosition = touchOne.position - touchOne.deltaPosition;

            Vector2 touchZeroRelativePosition = Camera.main.ScreenToWorldPoint(touchZero.position) - Camera.main.transform.position;
            Vector2 touchOneRelativePosition = Camera.main.ScreenToWorldPoint(touchOne.position) - Camera.main.transform.position;

            Vector2 touchMiddlePosition = (touchZeroRelativePosition + touchOneRelativePosition) / 2;

            float previousMagnitude = (touchZeroPreviousPosition - touchOnePreviousPosition).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - previousMagnitude;

            Zoom(difference / 20 * cameraZoomSensitivity);
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            float previousOrthographicSize = _virtualCamera.m_Lens.OrthographicSize;
            Zoom(Input.mouseScrollDelta.y / 20 * cameraZoomSensitivity);
            if (_virtualCamera.m_Lens.OrthographicSize != previousOrthographicSize)
            {
                _difference = _mainCamera.transform.position - _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                _lookPoint.transform.position += _difference / _mainCamera.orthographicSize / 2;
            }
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            float previousOrthographicSize = _virtualCamera.m_Lens.OrthographicSize;
            Zoom(Input.mouseScrollDelta.y / 20 * cameraZoomSensitivity);
            if (_virtualCamera.m_Lens.OrthographicSize != previousOrthographicSize)
            {
                _difference = _mainCamera.transform.position - _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                _lookPoint.transform.position -= _difference / _mainCamera.orthographicSize / 2;
            }
        }

    }
}
