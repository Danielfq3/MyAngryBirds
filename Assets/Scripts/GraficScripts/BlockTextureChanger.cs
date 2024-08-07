using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BlockTextureChanger : MonoBehaviour
{
    [SerializeField]
    private Sprite _spriteCracked;

    [SerializeField]
    private int _halfHealth;

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
        if (currentHealth < _halfHealth)
        {
            GetComponent<SpriteRenderer>().sprite = _spriteCracked;
        }
    }
}
