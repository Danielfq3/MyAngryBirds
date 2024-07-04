using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class TripleBird : Bird
{
    private bool _birdsSpawned;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_birdsSpawned)
            {
                return;
            }

            if (isBirdLaunched())
            {
                return;
            }
            
            var amountOfGameObjects = GameObject.FindObjectsOfType<TripleBird>();
            if (amountOfGameObjects.Length == 1)
            {
                var jo1 = Instantiate(gameObject, gameObject.transform.position, Quaternion.identity);
                jo1.GetComponent<Rigidbody2D>().velocity = GetVelocityForFirstBird();
                var jo2 = Instantiate(gameObject, gameObject.transform.position, Quaternion.identity);
                jo2.GetComponent<Rigidbody2D>().velocity = GetVelocityForSecondBird();
                _birdsSpawned = true;
            }
        }
    }

    private Vector3 GetVelocityForFirstBird()
    {
        var axis = new Vector3(0f, 0f, 1f);
        var angle = 20;
        return Quaternion.AngleAxis(angle, axis) * GetBirdVelocity();
    }

    private Vector3 GetVelocityForSecondBird()
    {
        var axis = new Vector3(0f, 0f, 1f);
        var angle = -20;
        return Quaternion.AngleAxis(angle, axis) * GetBirdVelocity();
    }

    private Vector2 GetBirdVelocity() => GetComponent<Rigidbody2D>().velocity;

    private bool isBirdLaunched()
    {
        return gameObject.GetComponent<Rigidbody2D>().isKinematic;
    }
}
