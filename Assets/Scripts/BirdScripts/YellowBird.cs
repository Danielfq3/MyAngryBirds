using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class YellowBird : Bird
{
    private bool _motionApplied;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_motionApplied)
            {
                return;
            }

            if (isBirdLaunched())
            {
                return;
            }

            gameObject.GetComponent<Rigidbody2D>().velocity = GetVelocityForGameObject();
            _motionApplied = true;
        }
    }

    private Vector3 GetVelocityForGameObject()
    {
        return gameObject.GetComponent<Rigidbody2D>().velocity * 2;
    }

    private Vector2 GetBirdVelocity() => GetComponent<Rigidbody2D>().velocity;

    private bool isBirdLaunched()
    {
        return gameObject.GetComponent<Rigidbody2D>().isKinematic;
    }
}
