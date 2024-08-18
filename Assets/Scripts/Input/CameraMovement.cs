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
    private BirdLauncher _birdLauncher;

    [SerializeField]
    private GameObject _lookPoint;

    [SerializeField]
    private float cameraZoomSensitivity = 1;

    private Vector2 _origin;
    private Vector3 _difference;
    private Vector2 _mousePosition;

    private Camera _mainCamera;


    private void Awake() => _mainCamera = Camera.main;

    void Zoom(float increment)
    {
        _virtualCamera.m_Lens.OrthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, minZoom, maxZoom);
    }

    private void Update()
    {
        TouchHandling();

        MouseHandling();

    }

    private void MouseHandling()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _origin = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            _mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _difference = (Vector2)_lookPoint.transform.position - _mousePosition;
            _lookPoint.transform.position = (Vector2)_difference + _origin;
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            float previousOrthographicSize = _virtualCamera.m_Lens.OrthographicSize;
            Zoom(Input.mouseScrollDelta.y / 20 * cameraZoomSensitivity);
            if (_virtualCamera.m_Lens.OrthographicSize != previousOrthographicSize)
            {
                _difference = _mainCamera.transform.position - _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                _lookPoint.transform.position += _difference / _mainCamera.orthographicSize / 20 * cameraZoomSensitivity;
            }
        }

        if (Input.mouseScrollDelta.y > 0)
        {
            float previousOrthographicSize = _virtualCamera.m_Lens.OrthographicSize;
            Zoom(Input.mouseScrollDelta.y / 20 * cameraZoomSensitivity);
            if (_virtualCamera.m_Lens.OrthographicSize != previousOrthographicSize)
            {
                _difference = _mainCamera.transform.position - _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                _lookPoint.transform.position -= _difference / _mainCamera.orthographicSize / 20 * cameraZoomSensitivity;
            }
        }
    }

    private void TouchHandling()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPreviousPosition = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePreviousPosition = touchOne.position - touchOne.deltaPosition;

            Vector2 touchZeroRelativePosition = _mainCamera.ScreenToWorldPoint(touchZero.position) - _mainCamera.transform.position;
            Vector2 touchOneRelativePosition = _mainCamera.ScreenToWorldPoint(touchOne.position) - _mainCamera.transform.position;

            Vector2 touchMiddlePosition = (touchZeroRelativePosition + touchOneRelativePosition) / 2;

            float previousMagnitude = (touchZeroPreviousPosition - touchOnePreviousPosition).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - previousMagnitude;

            if (difference > 0)
            {
                float previousOrthographicSize = _virtualCamera.m_Lens.OrthographicSize;
                Zoom(difference / 200 * cameraZoomSensitivity);
                if (_virtualCamera.m_Lens.OrthographicSize != previousOrthographicSize)
                {
                    _difference = (Vector2)_mainCamera.transform.position - touchMiddlePosition;
                    _lookPoint.transform.position += _difference / _mainCamera.orthographicSize / 2;
                }
            }
            if (difference < 0)
            {
                float previousOrthographicSize = _virtualCamera.m_Lens.OrthographicSize;
                Zoom(difference / 100 * cameraZoomSensitivity);
                if (_virtualCamera.m_Lens.OrthographicSize != previousOrthographicSize)
                {
                    _difference = _mainCamera.transform.position - _mainCamera.ScreenToWorldPoint(touchMiddlePosition);
                    _lookPoint.transform.position -= _difference / _mainCamera.orthographicSize / 2;
                }
            }
        }

        if (Input.touchCount == 1)
        {
            if (_birdLauncher.LaunchRadius > (_birdLauncher.LaunchPointPosition - (Vector2)_mainCamera.ScreenToWorldPoint(Input.GetTouch(0).rawPosition)).magnitude)
            {
                return;
            }

            if (Input.GetTouch(0).position == Input.GetTouch(0).rawPosition)
            {
                _origin = _mainCamera.ScreenToWorldPoint(Input.GetTouch(0).rawPosition);
            }
            _mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _difference = (Vector2)_lookPoint.transform.position - _mousePosition;
            _lookPoint.transform.position = (Vector2)_difference + _origin;
        }
    }
}
