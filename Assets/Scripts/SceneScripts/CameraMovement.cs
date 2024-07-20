using Cinemachine;
using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float minZoom = 2;
    [SerializeField]
    private float maxZoom = 12;

    private Vector3 dragOrigin;
    private Vector3 originalPos;

    [SerializeField]
    private float cameraZoomSensitivity = 12;
    private Vector3 touchStart;

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

            Zoom(difference / cameraZoomSensitivity);
            if (difference > 0)
            {
                Camera.main.transform.position += (Vector3)((touchMiddlePosition / (Camera.main.orthographicSize / cameraZoomSensitivity) - touchMiddlePosition) * ((Camera.main.orthographicSize - minZoom) / cameraZoomSensitivity));
            }

            if (difference < 0)
            {
                Camera.main.transform.position -= (Vector3)((touchMiddlePosition / (Camera.main.orthographicSize / cameraZoomSensitivity) - touchMiddlePosition) * ((Camera.main.orthographicSize - minZoom) / cameraZoomSensitivity));
            }

        }

        if (Input.GetMouseButtonDown(0) && Input.touchCount == 1)
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0) && Input.touchCount == 1)
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }

        if (Input.GetMouseButtonDown(1))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }

        if (Input.mouseScrollDelta.y > 0)
        {
            Zoom(Input.mouseScrollDelta.y);
            Vector2 mouseRelativePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position;
            Camera.main.transform.position += (Vector3)((mouseRelativePosition / (Camera.main.orthographicSize / cameraZoomSensitivity) - mouseRelativePosition) * ((Camera.main.orthographicSize - minZoom) / cameraZoomSensitivity));
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            Zoom(Input.mouseScrollDelta.y);
            Vector2 mouseRelativePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position;
            Camera.main.transform.position -= (Vector3)((mouseRelativePosition / (Camera.main.orthographicSize / cameraZoomSensitivity) - mouseRelativePosition) * ((Camera.main.orthographicSize - minZoom) / cameraZoomSensitivity));
        }
    }

    void Zoom(float increment)
    {
        gameObject.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, minZoom, maxZoom);
    }


}
