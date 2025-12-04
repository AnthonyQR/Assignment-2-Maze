using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 25f;
    private Rigidbody2D rb;
    private const float minXComponent = 0.5f;
    private bool inPlay = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Reset()
    {
        // Reset the ball to the center and stop its movement
        transform.position = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        Launch();
    }

    public void Launch()
    {
        // Launch the ball in a random direction
        float randomAngleX = Random.Range(0, 2) == 0 ? -1 : 1;
        float randomAngleY = Random.Range(0, 2) == 0 ? -1 : 1;

        rb.linearVelocity = new Vector2(speed * randomAngleX, speed * randomAngleY);
        inPlay = true;
    }

    public void FixedUpdate()
    {
        if (inPlay)
        {
            Vector2 direction = rb.linearVelocity.normalized;

            // Enforce minimum X component to prevent vertical bounces
            if (Mathf.Abs(direction.x) < minXComponent)
            {
                direction.x = Mathf.Sign(direction.x) * minXComponent;
                direction = direction.normalized;
            }
            // Ensure the ball maintains a constant speed
            rb.linearVelocity = direction * speed;
        }
    }
}
