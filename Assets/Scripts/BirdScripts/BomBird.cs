using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BomBird : Bird
{
    [SerializeField]
    private float _destroyRadiusMultiplier = 0.5f;
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
        StartCoroutine(DelayExplosion(3));
    }

    IEnumerator DelayExplosion(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Explode();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.GetComponent<Rigidbody2D>().isKinematic == false)
        {
            Explode();
        }
    }

    private void Explode()
    {
        foreach (var affectedObject in FindAllObjects())
        {
            if ((affectedObject.transform.position - gameObject.transform.position).magnitude < _explosionRadius * _destroyRadiusMultiplier)
            {
                print("booooom");
                affectedObject.GetComponent<HealthForObjects>().SetHealth(0);
            }
        }
        foreach (var affectedObject in FindAllObjects())
        {
            if ((affectedObject.transform.position - gameObject.transform.position).magnitude < _explosionRadius)
            {
                Vector3 boomDirection = affectedObject.transform.position - gameObject.transform.position;
                float boomForce = _explosionForce * 1000 / boomDirection.sqrMagnitude;
                boomForce *= affectedObject.GetComponent<Rigidbody2D>().mass;
                affectedObject.GetComponent<Rigidbody2D>().AddForce(boomDirection * boomForce);
            }
        }
        Destroy(gameObject);
        BirdDestroyed();
    }
}
