using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class WhiteBird : Bird
{
    [SerializeField]
    private GameObject _egg;

    [SerializeField]
    private float _changeVectorAfterEggLaunched;
    
    [SerializeField]
    private Vector2 _launchForceForEgg = new Vector2(0, -9.81f);
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
            
            var amountOfWhiteBirds = GameObject.FindObjectsOfType<WhiteBird>();
            if (amountOfWhiteBirds.Length == 1)
            {
                var jo1 = Instantiate(_egg, gameObject.transform.position ,Quaternion.identity);
                jo1.GetComponent<Rigidbody2D>().velocity = GetVelocityForEgg();
                gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(0f, _changeVectorAfterEggLaunched);
                _birdsSpawned = true;
            }
        }
    }

    private Vector3 GetVelocityForEgg()
    {
        return _launchForceForEgg;
    }

    private bool isBirdLaunched()
    {
        return gameObject.GetComponent<Rigidbody2D>().isKinematic;
    }
}
