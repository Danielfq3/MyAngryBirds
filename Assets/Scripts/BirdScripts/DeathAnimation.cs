using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    [SerializeField]
    public GameObject popPrefab;

    private void Awake()
    {
        GetComponent<Bird>().OnBirdDestroyed += OnBirdDestroyed;
    }

    private void OnBirdDestroyed()
    {
        GameObject popObject = Instantiate(popPrefab, transform.position, Quaternion.identity);
    }
}
