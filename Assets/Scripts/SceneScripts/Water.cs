using UnityEngine;

public class SlowDownOnTrigger : MonoBehaviour
{
    [SerializeField]
    public float slowDownFactor = 1.0f; // Adjust this value as needed

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject)
        {
            var otherObjectRB = other.gameObject.GetComponent<Rigidbody2D>();
            // Set linear drag to slow down the object
            otherObjectRB.drag = slowDownFactor;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other)
        {
            var otherObjectRB = other.gameObject.GetComponent<Rigidbody2D>();
            // Reset linear drag when exiting the trigger
            otherObjectRB.drag = 0f;
        }
    }
}
