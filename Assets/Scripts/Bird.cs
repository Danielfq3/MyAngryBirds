using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public event Action OnBirdDestroyed = delegate { };
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke(nameof(BirdDestroyed), 4);
        Destroy(gameObject, 4);
    }

    private void BirdDestroyed()
    {
        OnBirdDestroyed();
    }
}
