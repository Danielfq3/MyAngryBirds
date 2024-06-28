using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private void Update()
    {
        Destroy(gameObject, 15);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, 2);
    }
}
