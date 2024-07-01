using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public event Action OnBirdDestroyed = delegate { };
    public event Action OnBirdCollided = delegate { };
    private void Awake()
    {
        if (gameObject.GetComponent<Rigidbody2D>().isKinematic == false)
        {
            Destroy(gameObject, 1);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnBirdCollided();
        Invoke(nameof(BirdDestroyed), 4);
        Destroy(gameObject, 4);
    }

    protected void BirdDestroyed()
    {
        OnBirdDestroyed();
    }
}
