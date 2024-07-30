using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HealthIndicator : MonoBehaviour
{
    [SerializeField]
    private Sprite _spriteCracked1;
    [SerializeField]
    private Sprite _spriteCracked2;

    private HealthForObjects _healthForObjects;

    private void Awake()
    {
        _healthForObjects = GetComponent<HealthForObjects>();
    }


    private void OnEnable()
    {
        _healthForObjects.OnHealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _healthForObjects.OnHealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int currentHealth)
    {
        if (currentHealth == 2)
        {
            GetComponent<SpriteRenderer>().sprite = _spriteCracked1;
        }
        
        if (currentHealth == 1)
        {
            GetComponent<SpriteRenderer>().sprite = _spriteCracked2;
        }
    }
}
