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
        _isDragging = ctx.started || ctx.performed;
    }

    private void LateUpdate()
    {
        if (!_isDragging) return;

        _difference = GetMousePosition - transform.position;
        transform.position = _origin - _difference;
    }

    private Vector3 GetMousePosition => _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

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

        /*if (Input.GetMouseButtonDown(1))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }*/

        if (Input.mouseScrollDelta.y != 0)
        {
            Zoom(Input.mouseScrollDelta.y);
        }

        void Zoom(float increment)
        {
            return;
            gameObject.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, minZoom, maxZoom);
        }


    }
}
