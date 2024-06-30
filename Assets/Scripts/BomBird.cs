using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomBird : Bird
{
    private HealthForObjects[] FindAllPlanks()
    {
        return Object.FindObjectsByType<HealthForObjects>(FindObjectsSortMode.None);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.GetComponent<Rigidbody2D>().isKinematic == false)
        {
            foreach (var plank in FindAllPlanks())
            {
                if((plank.transform.position - gameObject.transform.position).magnitude < 8)
                {
                    Vector3 boomDirection = gameObject.transform.position - plank.transform.position;
                    float boomStrenght = 50000 / boomDirection.sqrMagnitude;
                    gameObject.GetComponent<Rigidbody2D>().mass = boomStrenght;
                    plank.GetComponent<Rigidbody2D>().AddForce(boomDirection * boomStrenght);
                    Destroy(gameObject);
                }
            }
        }
    }
}
