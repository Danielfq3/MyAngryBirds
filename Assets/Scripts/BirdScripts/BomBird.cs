using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BomBird : Bird
{
    [SerializeField]
    private float _DestroyMultiplier = 3;
    [SerializeField]
    private float _explosionRadius = 4;
    [SerializeField]
    private int _explosionForce = 10;
    [SerializeField]
    private GameObject _explosionObject;
    [SerializeField]
    private Material _explosionMaterial;

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
        StartCoroutine(DelayExplosion(1.9f));
    }

    IEnumerator DelayExplosion(float seconds)
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
        _explosionObject.transform.position = gameObject.transform.position;
        _explosionObject.SetActive(true);
        Destroy(gameObject);
        BirdDestroyed();
    }
}
