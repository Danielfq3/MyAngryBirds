using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIndicator : MonoBehaviour
{
    private void Start()
    {
        GetComponent<HealthForObjects>().OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int currentHealth)
    {
        print(currentHealth);
        gameObject.GetComponent<Renderer>().material.color = new Color((float)currentHealth / 3, 0, 0, 1);
    }
}
