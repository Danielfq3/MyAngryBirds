using System;
using System.Collections;
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
    [SerializeField]
    float launchRadius = 3;

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
        if (Input.GetMouseButtonDown(0) && ((Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - launchPoint.position)).magnitude < launchRadius && _launchState == State.ReadyToLaunch)
        {
            StartDrag();
            _launchState = State.Disabled;
        }
        if (isDragging)
        {
            Drag();
            ChangeStringPosition();
        }
        if (Input.GetMouseButtonUp(0) && isDragging && CalculateDragVector().magnitude > 0.2)
        {
            Release();
            ResetStringPosition();
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

    private void ChangeStringPosition()
    {
        gameObject.GetComponent<LineRenderer>().SetPosition(1, -(CalculateDragVector() * 1.6f - new Vector3(0, 1.20f, 0.5f)));
    }

    private void ResetStringPosition()
    {
        gameObject.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 1.27f, 0.5f));
    }
    
    private IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        _launchState = State.ReadyToLaunch;
    }

    private void OnBirdDestroyed()
    {
        StartCoroutine(ExecuteAfterTime(0.1f));
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
        currentBird.GetComponent<Bird>().launched();
        OnBirdLaunched();
        _birdsPool.Remove(currentBird);
        ParticleActivator particleActivator = currentBird.GetComponent<ParticleActivator>();
        particleActivator.EnableParticles();
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
