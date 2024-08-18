using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class BirdLauncher : MonoBehaviour
{
    private const float SlingTension = 0.3f;
    public GameObject birdPrefab;
    public Transform launchPoint;
    public float launchForce = 500f;

    private GameObject currentBird;
    private bool isDragging = false;
    private Vector3 initialPosition;
    [SerializeField]
    private float _maxMagnitude;
    [SerializeField]
    private float _minMagnitude = 0.2f;
    [SerializeField]
    private float _launchRadius = 3;
    [SerializeField]
    private List<GameObject> _birdsPool;

    [SerializeField]
    private LineRenderer _sling1;
    [SerializeField]
    private LineRenderer _sling2;

    private Vector3 _slingOffset;

    public float LaunchRadius => _launchRadius;

    public Vector2 LaunchPointPosition => launchPoint.position;
    public float MaxMagnitudeOfSlingshot => _maxMagnitude;
    public float MinMagnitudeOfSlingshot => _minMagnitude;

    private enum State
    {
        ReadyToLaunch,
        Disabled
    }

    private State _launchState = State.ReadyToLaunch;

    public event Action OnBirdLaunched = delegate { };
    public event Action OnBirdStartLaunching = delegate { };


    private void Awake()
    {
        _slingOffset = GetSlingOffset();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && ((Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - launchPoint.position)).magnitude < _launchRadius && _launchState == State.ReadyToLaunch)
        {
            StartDrag();
            _launchState = State.Disabled;
        }
        if (isDragging)
        {
            Drag();
            ChangeSlingPosition();
        }
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            if (CalculateDragVector().magnitude < _minMagnitude)
            {
                _launchState = State.ReadyToLaunch;
                isDragging = false;
                return;
            }
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

    private Vector3 GetSlingOffset() => _sling1.GetPosition(0);

    private void ChangeSlingPosition()
    {
        _sling1.SetPosition(0, -(CalculateDragVector() + CalculateDragVector().normalized * SlingTension - _slingOffset));
        _sling2.SetPosition(0, -(CalculateDragVector() + CalculateDragVector().normalized * SlingTension - _slingOffset));
    }

    private void ResetStringPosition()
    {
        _sling1.SetPosition(0, _slingOffset);
        _sling2.SetPosition(0, _slingOffset);

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
        currentBird.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, CalculateDragRotation()));
    }

    public Vector3 CalculateDragVector()
    {
        Vector3 dragVector = initialPosition - GetMouseWorldPosition();
        if (dragVector.magnitude > _maxMagnitude)
        {
            dragVector = dragVector.normalized * _maxMagnitude;
        }

        return dragVector;
    }

    private float CalculateDragRotation()
    {
        Vector2 dragVector = CalculateDragVector();
        float dragAngle = -Vector2.SignedAngle(dragVector, new Vector2(1, 0));
        return dragAngle;
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

    internal float GetBirdMass()
    {
        return currentBird.GetComponent<Rigidbody2D>().mass;
    }

    internal bool IsDragging()
    {
        return isDragging;
    }
}
