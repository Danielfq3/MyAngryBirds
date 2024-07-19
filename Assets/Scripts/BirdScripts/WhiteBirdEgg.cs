using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WhiteBirdEgg : Bird
{
    [SerializeField]
    private float _DestroyMultiplier = 3;
    [SerializeField]
    private float _explosionRadius = 4;
    [SerializeField]
    private int _explosionForce = 10;

    private HealthForObjects[] FindAllObjects()
    {
        return Object.FindObjectsByType<HealthForObjects>(FindObjectsSortMode.None);
    }

    private void OnEnable()
    {
        OnBirdCollided += BirdCollided;
    }
    private void OnDisable()
    {
        OnBirdCollided -= BirdCollided;
    }

    private void BirdCollided()
    {
        Explode();
    }

    private void Explode()
    {
        foreach (var affectedObject in FindAllObjects())
        {
            if ((affectedObject.transform.position - gameObject.transform.position).magnitude < _explosionRadius)
            {
                Vector3 boomDirection = affectedObject.transform.position - gameObject.transform.position;
                float boomForce = _explosionForce * 1000 / boomDirection.sqrMagnitude;
                float destroyForce = _DestroyMultiplier / boomDirection.magnitude;
                boomForce *= affectedObject.GetComponent<Rigidbody2D>().mass;
                print(destroyForce);
                affectedObject.GetComponent<HealthForObjects>().SubtractHealth((int)destroyForce);
                affectedObject.GetComponent<Rigidbody2D>().AddForce(boomDirection * boomForce);
            }
        }
        Destroy(gameObject);
        BirdDestroyed();
    }
}
