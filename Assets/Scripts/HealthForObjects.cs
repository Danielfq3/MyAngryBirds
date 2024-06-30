using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthForObjects : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth = 100f;

    [SerializeField]
    private float _breakForce;
    private float _currentHealth;

    private Vector3 GetPlankPosition()
    {
        return gameObject.transform.position;
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude * collision.otherRigidbody.mass > _breakForce)
        {
            _currentHealth -= collision.relativeVelocity.magnitude;
            if (_currentHealth <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}

