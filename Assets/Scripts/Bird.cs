using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public event Action OnBirdDestroyed = delegate { };
    private void Update()
    {
        Destroy(gameObject, 15);
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
