using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
            Instantiate(gameObject, gameObject.transform.position, Quaternion.identity);
            Instantiate(gameObject, gameObject.transform.position, Quaternion.identity);
            _birdsSpawned = true;
        }
    }

    private bool isBirdLaunched()
    {
        return gameObject.GetComponent<Rigidbody2D>().isKinematic;
    }
}
