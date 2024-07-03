using System;
using System.Collections.Generic;
using System.Threading;
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
    [SerializeField]
    private List<GameObject> _birdsPool;

    private enum State
    {
        ReadyToLaunch,
        Disabled
    }

    private State _launchState = State.ReadyToLaunch;

    public event Action OnBirdLaunched = delegate { };
    public event Action OnBirdStartLaunching = delegate { };

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _launchState == State.ReadyToLaunch)
        {
            StartDrag();
            _launchState = State.Disabled;
        }
        if (isDragging)
        {
            Drag();
        }
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            Release();
        }
    }

    private static Vector3 GetCameraPosition()
    {
        return Camera.main.transform.position;
    }

    void StartDrag()
    {
        currentBird = _birdsPool[0];
        currentBird.transform.position = launchPoint.transform.position;
        currentBird.GetComponent<Rigidbody2D>().simulated = true;
        SetGravityStatusFor(currentBird, false);
        initialPosition = GetMouseWorldPosition();
        isDragging = true;
        currentBird.GetComponent<Bird>().OnBirdDestroyed += OnBirdDestroyed;
        OnBirdStartLaunching();

    }

    private void OnBirdDestroyed()
    {
        StartCoroutine(ExecuteAfterTime(1));
        IEnumerator ExecuteAfterTime(float time)
        {
            yield return new WaitForSeconds(time);
            _launchState = State.ReadyToLaunch;
        }
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
        Vector3 dragVector = CalculateDragVector();
        currentBird.transform.position = launchPoint.position - dragVector;
    }

    private Vector3 CalculateDragVector()
    {
        Vector3 dragVector = initialPosition - GetMouseWorldPosition();
        if (dragVector.magnitude > _maxMagnitude)
        {
            dragVector = dragVector.normalized * _maxMagnitude;
        }

        return dragVector;
    }

    void Release()
    {
        isDragging = false;
        float currentBirdMass = currentBird.GetComponent<Rigidbody2D>().mass;
        Vector3 launchDirection = launchPoint.position - currentBird.transform.position;
        SetGravityStatusFor(currentBird, true);
        currentBird.GetComponent<Rigidbody2D>().AddForce(launchDirection * launchForce * currentBirdMass);
        OnBirdLaunched();
        _birdsPool.Remove(currentBird);
    }

    internal Vector3 GetBirdPosition()
    {
        return currentBird.transform.position;
    }

    internal bool IsBirdDestroyed()
    {
        return currentBird == null;
    }
}
