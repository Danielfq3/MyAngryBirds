using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector2 lastMousePosition;

    private Vector2 lastCameraPosition;

    private bool dragMovementActive;


    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            lastCameraPosition = Camera.main.transform.position;
            dragMovementActive = true;
            lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(1))
        {
            dragMovementActive = false;
        }


        if (dragMovementActive)
        {
            Vector2 mouseMovementDelta = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastMousePosition;
            print(mouseMovementDelta);
            gameObject.transform.position = lastCameraPosition -mouseMovementDelta;
        }
    }
}
