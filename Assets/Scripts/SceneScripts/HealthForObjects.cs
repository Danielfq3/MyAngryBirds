using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class HealthForObjects : MonoBehaviour
{
    [SerializeField]
    private int _collisionForceMultiplier = 100;
    [SerializeField]
    private int _breakForceMultiplier = 100;
    [SerializeField]
    private float BirdDamageMultiplier = 4;

    [SerializeField]
    private int _maxHealth = 3;

    [SerializeField]
    private float _breakForce;
    [SerializeField]
    private int _currentHealth;

    public event Action<int> OnHealthChanged = delegate { };

    private ScoreCounter FindScoreCounterObject()
    {
        return FindObjectOfType<ScoreCounter>(includeInactive:true);
    }

    public static Action OnObjectDestroyed = delegate { };
    private int _previousValue;

    private void Awake()
    {
        _previousValue = _currentHealth;
    }

    public void SubtractHealth(int health) => _currentHealth -= health;

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
        if (collision.gameObject.tag == "Bumper")
        {
            return;
        }
        if (collision.relativeVelocity.magnitude * collision.otherRigidbody.mass / _breakForceMultiplier <= _breakForce)
        {
            return;
        }
        float collisionForce = collision.relativeVelocity.magnitude * collision.otherRigidbody.mass;
        if (collision.gameObject.tag == "Bird")
        {
            collisionForce *= BirdDamageMultiplier;
        }
        collisionForce /= _collisionForceMultiplier;
        _currentHealth -= ((int)collisionForce);
        if (_currentHealth <= 0f)
        {
            FindScoreCounterObject().GetComponent<ScoreCounter>().AddScore(gameObject.tag);
            Destroy(gameObject);
            OnObjectDestroyed();
        }
    }

    private void Update()
    {
        if (_currentHealth != _previousValue)
        {
            OnHealthChanged(_currentHealth);
            //command type here
            // Execute additional code here
            // ...
            _previousValue = _currentHealth; // Update the previous value
        }

        if (_currentHealth <= 0)
        {
            //FindScoreCounterObject().GetComponent<ScoreCounter>().AddScore(gameObject.tag);
            Destroy(gameObject);
            OnObjectDestroyed();
        }
        if (gameObject.transform.position.magnitude > 1000)
        {
            FindScoreCounterObject().GetComponent<ScoreCounter>().AddScore(gameObject.tag);
            Destroy(gameObject);
            OnObjectDestroyed();
        }
    }
}
