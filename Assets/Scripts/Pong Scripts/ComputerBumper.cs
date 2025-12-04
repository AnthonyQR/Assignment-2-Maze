using UnityEngine;

public class ComputerBumper : MonoBehaviour
{
    private float speed = 15f;
    private Rigidbody2D ball;
    private Rigidbody2D rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetBall(Rigidbody2D ballRigidbody)
    {
        ball = ballRigidbody;
    }

    private void FixedUpdate()
    {
        // Check if the ball is moving towards the paddle (positive x velocity) or away from the paddle (negative x velocity)
        if (ball.linearVelocity.x > 0f)
        {
            // Move the paddle in the direction of the ball to track it
            if (ball.position.y > rb.position.y)
            {
                rb.linearVelocity = new Vector2(0f, 1 * speed);
            }
            else if (ball.position.y < rb.position.y)
            {
                rb.linearVelocity = new Vector2(0f, -1 * speed);
            }
        }
        else
        {
            // Move towards the center of the field and idle there until the ball starts coming towards the paddle again
            if (rb.position.y > 0f)
            {
               rb.AddForce(Vector2.down * speed / 2);
            }
            else if (rb.position.y < 0f)
            {
                rb.AddForce(Vector2.up * speed / 2);
            }
        }
    }

}
