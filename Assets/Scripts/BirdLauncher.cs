using System;
using UnityEngine;

public class BirdLauncher : MonoBehaviour
{
    public GameObject birdPrefab;
    public Transform launchPoint;
    public float launchForce = 500f;

    private GameObject currentBird;
    private bool isDragging = false;
    private Vector3 initialPosition;
    [SerializeField]
    private float _maxMagnitude;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrag();
        }
        if (isDragging)
        {
            Drag();
        }
        if (Input.GetMouseButtonUp(0))
        {
            Release();
        }
    }

    void StartDrag()
    {
        currentBird = Instantiate(birdPrefab, launchPoint.position, Quaternion.identity);
        SetGravityStatusFor(currentBird, false);
        initialPosition = GetMouseWorldPosition();
        isDragging = true;
    }

    private void SetGravityStatusFor(GameObject currentBird, bool status)
    {
        currentBird.GetComponent<Rigidbody2D>().isKinematic = !status;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMousePosition.z = 0f;
        return worldMousePosition;
    }

    void Drag()
    {
        Vector3 dragVector = initialPosition - GetMouseWorldPosition();
        if (dragVector.magnitude > _maxMagnitude)
        {
            dragVector = dragVector.normalized * _maxMagnitude;
        }
        currentBird.transform.position = launchPoint.position - dragVector;
    }

    void Release()
    {
        isDragging = false;
        Vector3 launchDirection = launchPoint.position - currentBird.transform.position;
        SetGravityStatusFor(currentBird, true);
        currentBird.GetComponent<Rigidbody2D>().AddForce(launchDirection * launchForce);
    }
}
