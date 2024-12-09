using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        
        if (rb == null)
        {
            return;
        }

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = Mathf.Round(velocity.x);
        velocity.y = Mathf.Round(velocity.y);
        rb.velocity = velocity;
    }
}
