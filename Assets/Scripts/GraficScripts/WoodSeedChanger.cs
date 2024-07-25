using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSeedChanger : MonoBehaviour
{


    private void OnEnable()
    {
        float random = UnityEngine.Random.Range(-6.5f, 6.5f);
        var renderer = GetComponent<SpriteRenderer>();
        renderer.material.SetFloat("_Seed", random);
    }
}
