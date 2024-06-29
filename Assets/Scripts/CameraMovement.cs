using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private BirdLauncher _birdLauncher;
    private Vector3 GetCameraPosition() => Camera.main.transform.position;

    private void SetCameraPosition(Vector3 position) => Camera.main.transform.position = position;

    private bool _cameraMoving = false;
    private Vector3 GetVectorToBird()
    {
        return (GetBirdPosition() - GetCameraPosition()).normalized * 0.01f;
    }

    private void Start()
    {
        _birdLauncher.OnBirdLaunched += OnBirdLaunched;
        _birdLauncher.OnBirdStartLaunching += OnBirdStartLaunching;
    }

    private void OnBirdStartLaunching()
    {
        _cameraMoving = false;
    }

    private void OnBirdLaunched()
    {
        _cameraMoving = true;
    }

    private Vector3 GetBirdPosition() => _birdLauncher.GetBirdPosition();

    // Update is called once per frame
    void Update()
    {
        if (_birdLauncher.IsBirdDestroyed())
        {
            _cameraMoving = false;
        }
        if (_cameraMoving)
        {
            SetCameraPosition(GetCameraPosition() + new Vector3(GetVectorToBird().x, 0f, 0f));
        }
    }
}
