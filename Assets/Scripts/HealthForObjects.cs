using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthForObjects : MonoBehaviour
{
    [SerializeField]
    private float BirdDamageMultiplier = 4;
    [SerializeField]
    private float _maxHealth = 100f;

    [SerializeField]
    private float _breakForce;
    private float _currentHealth;

    private Vector3 GetObjectPosition()
    {
        return gameObject.transform.position;
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude * collision.otherRigidbody.mass <= _breakForce)
        {
            return;
        }
        float collisionForce = collision.relativeVelocity.magnitude * collision.otherRigidbody.mass;
        if (collision.gameObject.tag == "Bird")
        {
            collisionForce *= BirdDamageMultiplier;
        }
        print(collision.gameObject.tag == "Bird");
        _currentHealth -= collisionForce;
        if (_currentHealth <= 0f)
        {
            Destroy(gameObject);
        }
    }
}

