using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleActivator : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;

    public void EnableParticles()
    {
        _particleSystem.Play();
    }
}
