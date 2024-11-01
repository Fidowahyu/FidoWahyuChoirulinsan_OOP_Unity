using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 maxSpeed;
    [SerializeField] private Vector2 timeToFullSpeed;
    [SerializeField] private Vector2 timeToStop;
    [SerializeField] private Vector2 stopClamp;

    private Vector2 moveVelocity;
    private Vector2 moveFriction;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveVelocity = maxSpeed / timeToFullSpeed; 
        moveFriction = -maxSpeed / timeToStop; 
    }

    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        Vector2 moveDirection = new Vector2(inputX, inputY).normalized;

        if (moveDirection != Vector2.zero)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + moveVelocity * moveDirection * Time.fixedDeltaTime, maxSpeed.magnitude);
        }
        else
        {
            ApplyFriction();
        }
    }

    private void ApplyFriction()
    {
        if (rb.velocity.magnitude > stopClamp.magnitude)
        {
            rb.velocity += moveFriction * Time.fixedDeltaTime;
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, stopClamp.magnitude);
        }
        else
        {
            rb.velocity = Vector2.zero; 
        }
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0.1f;
    }

}
