using UnityEngine;

public class FanController : MonoBehaviour
{
    public float fanForce = 200f; // Adjust the force as needed

    private void OnTriggerStay2D(Collider2D other)
    {
        // Check if the colliding object has a Rigidbody2D
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Apply force away from the fan
            Vector2 forceDirection = transform.up; // Change this if needed
            rb.AddForce(forceDirection * fanForce, ForceMode2D.Force);
        }
    }
}
