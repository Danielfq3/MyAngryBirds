using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BomBird : Bird
{
    [SerializeField]
    private float _explosionRadius = 4;
    [SerializeField]
    private int _explosionForce = 10000;

    private HealthForObjects[] FindAllPlanks()
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
        foreach (var plank in FindAllPlanks())
        {
            if ((plank.transform.position - gameObject.transform.position).magnitude < _explosionRadius / 1.5)
            {
                Destroy(plank.gameObject);
            }
        }
        foreach (var plank in FindAllPlanks())
        {
            if ((plank.transform.position - gameObject.transform.position).magnitude < _explosionRadius)
            {
                Vector3 boomDirection = plank.transform.position - gameObject.transform.position;
                float boomStrenght = _explosionForce / boomDirection.sqrMagnitude;
                gameObject.GetComponent<Rigidbody2D>().mass = boomStrenght;
                plank.GetComponent<Rigidbody2D>().AddForce(boomDirection * boomStrenght);
            }
        }
        Destroy(gameObject);
        BirdDestroyed();
    }
}
