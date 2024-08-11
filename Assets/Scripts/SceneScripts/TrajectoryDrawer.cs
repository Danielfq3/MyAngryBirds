using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class TrajectoryDrawer : MonoBehaviour
{
    private const float MinScaleOfTrajectoryPoint = 0.2f;
    private const float MinAlphaOfTrajectoryPoint = 0.1f;
    [SerializeField]
    private float _initialPointScale = 0.4f;
    [SerializeField]
    private int _maxTrajectorySteps = 16;
    [SerializeField]
    private int _trajectoryDensity = 16;
    [SerializeField]
    private BirdLauncher _birdLauncher;
    private Vector2 _birdPosition;
    [SerializeField]
    private Transform _launchPoint;
    private Vector2 _launchPosition;
    private float _birdMass;
    private Vector2 _gravity;
    private float _launchForce = 16;
    [SerializeField]
    private GameObject _point;
    private List<GameObject> _points = new List<GameObject>();

    // y = y0 + vt + ( g * t^2 ) / 2
    private Vector2 PredictBirdPosition(float time)
    {
        _gravity = Physics2D.gravity;
        _birdMass = _birdLauncher.GetBirdMass();
        _launchPosition = _launchPoint.position;
        _birdPosition = _birdLauncher.GetBirdPosition();
        Vector2 velocity = (_launchPosition - _birdPosition) * _launchForce;
        return _birdPosition + velocity * time + (_gravity * time * time) / 2;
    }


    private void CreatePointArray()
    {
        for (int i = 0; i < _maxTrajectorySteps; i++)
        {
            GameObject point = Instantiate(_point);
            _points.Add(point);
        }
    }

    private int CalculateAmountOfSteps()
    {
        return (int)(Math.Max(1, _maxTrajectorySteps * Math.Pow((_birdLauncher.CalculateDragVector().magnitude / _birdLauncher.MaxMagnitudeOfSlingshot), 2)));
    }

    private void TransformPointArray()
    {
        _launchPosition = _launchPoint.position;
        _birdPosition = _birdLauncher.GetBirdPosition();
        for (int i = 0; i < _maxTrajectorySteps; i++)
        {
            GameObject point = _points[i];
            point.SetActive(false);
            if (i < CalculateAmountOfSteps() && _birdLauncher.CalculateDragVector().magnitude >= _birdLauncher.MinMagnitudeOfSlingshot)
            {
                point.SetActive(true);
                point.transform.position = PredictBirdPosition(CalculatePointPosition(i));
                ScalePoint(point, i);
            }
        }
    }

    private void ScalePoint(GameObject point, int step)
    {
        point.transform.localScale = Vector3.Lerp(Vector3.one * _initialPointScale, Vector3.one * _initialPointScale * MinScaleOfTrajectoryPoint, (float)(step + 1) / Math.Max(1, CalculateAmountOfSteps() - 1));
    }

    private float CalculatePointPosition(int step) =>
        ((float)step + 1) / (Mathf.Clamp((_launchPosition - _birdPosition).magnitude, _birdLauncher.MinMagnitudeOfSlingshot, _birdLauncher.MaxMagnitudeOfSlingshot) / _birdLauncher.MaxMagnitudeOfSlingshot * _trajectoryDensity); 

    private void Start()
    {
        CreatePointArray();
    }

    private void Update()
    {
        if (_birdLauncher.IsDragging())
        {
            TransformPointArray();
        }
    }
}
