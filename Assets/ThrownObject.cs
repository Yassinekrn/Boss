using UnityEngine;

public class ThrownObject : MonoBehaviour
{
    private Rigidbody2D rb;
    public float minimumSpeed = 0.1f;
    public float speedDecayRate = 0.1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Check if the object's velocity magnitude is below the minimum speed threshold
        if (rb.velocity.magnitude <= minimumSpeed)
        {
            // If the object's velocity is below the threshold, destroy the object
            Destroy(gameObject);
        }
        else
        {
            // Gradually reduce the object's speed over time
            rb.velocity -= rb.velocity.normalized * speedDecayRate * Time.deltaTime;
        }
    }

    // Other methods and code specific to your thrown object's behavior
}
