using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public event Action OnBirdDestroyed = delegate { };
    private void Awake()
    {
        if (gameObject.GetComponent<Rigidbody2D>().isKinematic == false)
        {
            Destroy(gameObject, 1);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke(nameof(BirdDestroyed), 2);
        Destroy(gameObject, 2);
    }

    private void BirdDestroyed()
    {
        OnBirdDestroyed();
    }
}
