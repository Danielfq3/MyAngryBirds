using System;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private HealthForObjects _healthForObjects;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _healthForObjects = GetComponent<HealthForObjects>();
        _healthForObjects.OnHealthChanged += OnHealthChanged;
        _particleSystem.Stop();
    }

    private void OnHealthChanged(int hp)
    {
        SpawnParticles();
    }

    public void SpawnParticles()
    {

        // Start the Particle System
        _particleSystem.Play();
    }


}