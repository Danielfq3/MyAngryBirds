using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleBird : Bird
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(gameObject, gameObject.transform.position, Quaternion.identity);
            Instantiate(gameObject, gameObject.transform.position, Quaternion.identity);
        }
    }
}
