using Cinemachine;
using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 touchStart;
    private float minZoom = 2;
    private float maxZoom = 12; 


    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }
        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    void Zoom(float increment)
    {
        gameObject.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, minZoom, maxZoom);
    }


}
