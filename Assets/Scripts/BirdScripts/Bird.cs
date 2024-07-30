using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public event Action OnBirdDestroyed = delegate { };
    public event Action OnBirdCollided = delegate { };
    [SerializeField]
    public Sprite spriteCracked;
    [SerializeField]
    public Sprite _spriteFlying;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnBirdCollided();
        GetComponent<SpriteRenderer>().sprite = spriteCracked;
        Invoke(nameof(BirdDestroyed), 2);
        Destroy(gameObject, 2);
    }

    protected void BirdDestroyed()
    {
        OnBirdDestroyed();
    }

    public void launched()
    {
        GetComponent<SpriteRenderer>().sprite = _spriteFlying;
    }
}
