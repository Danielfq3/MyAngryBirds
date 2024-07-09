using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WhiteBirdEgg : Bird
{
    [SerializeField]
    private float _explosionRadius = 4;
    [SerializeField]
    private int _explosionForce = 100000;

    private HealthForObjects[] FindAllPlanks()
    {
        return Object.FindObjectsByType<HealthForObjects>(FindObjectsSortMode.None);
    }

    private void OnEnable()
    {
        OnBirdCollided += EggCollided;
    }
    private void OnDisable()
    {
        OnBirdCollided -= EggCollided;
    }

    private void EggCollided()
    {
        Explode();
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
