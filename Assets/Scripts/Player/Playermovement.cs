using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement With Time")]
    [SerializeField] Vector2 maxSpeed = new(10f, 10f);
    [SerializeField] Vector2 timeToFullSpeed = new(2f, 2f);
    [SerializeField] Vector2 timeToStop = new(2.5f, 2.5f);
    [SerializeField] Vector2 stopClamp = new(2.5f, 2.5f);

    [Header("Movement Calculation")]
    Vector2 moveDirection;
    Vector2 moveVelocity;
    Vector2 moveFriction;
    Vector2 stopFriction;
    Vector2 ppos;

    [Header("Player Components")]
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveVelocity = 2.0f * maxSpeed / timeToFullSpeed;
        moveFriction = -2.0f * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2.0f * maxSpeed / (timeToStop * timeToStop);
    }

    public void Move()
    {
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rb.velocity += moveVelocity * Time.deltaTime * moveDirection;
        rb.velocity = new(Mathf.Clamp(rb.velocity.x, -maxSpeed.x, maxSpeed.x), Mathf.Clamp(rb.velocity.y, -maxSpeed.y, maxSpeed.y));
        rb.velocity -= GetFriction() * Time.deltaTime;

        if (Math.Abs(rb.velocity.x) < stopClamp.x && moveDirection.x == 0 || moveDirection.x > -0 && ppos.x >= 0.99f || moveDirection.x < 0 && ppos.x <= 0.01f)
            rb.velocity = new(0, rb.velocity.y);

        if (Math.Abs(rb.velocity.y) < stopClamp.y && moveDirection.y == 0 || moveDirection.y > -0 && ppos.y >= 0.95f || moveDirection.y < 0 && ppos.y <= -0.01f)
            rb.velocity = new(rb.velocity.x, 0);

        MoveBound();
    }

    public Vector2 GetFriction()
    {
        Vector2 totalFriction = Vector2.zero;

        if (moveDirection.x > 0)
            totalFriction.x = moveFriction.x;
        else if (moveDirection.x < 0)
            totalFriction.x = -moveFriction.x;
        else if (moveDirection.x == 0 && rb.velocity.x > 0)
            totalFriction.x = -stopFriction.x;
        else if (moveDirection.x == 0 && rb.velocity.x < 0)
            totalFriction.x = stopFriction.x;
        else
            totalFriction.x = 0;

        if (moveDirection.y > 0)
            totalFriction.y = moveFriction.y;
        else if (moveDirection.y < 0)
            totalFriction.y = -moveFriction.y;
        else if (moveDirection.y == 0 && rb.velocity.y > 0)
            totalFriction.y = -stopFriction.y;
        else if (moveDirection.y == 0 && rb.velocity.y < 0)
            totalFriction.y = stopFriction.y;
        else
            totalFriction.y = 0;

        return totalFriction;
    }

    public void MoveBound()
    {
        ppos = Camera.main.WorldToViewportPoint(transform.position);
        ppos.x = Mathf.Clamp(ppos.x, 0.01f, 0.99f);
        ppos.y = Mathf.Clamp(ppos.y, -0.01f, 0.95f);
        transform.position = Camera.main.ViewportToWorldPoint(ppos) + new Vector3(0, 0, 10);
    }

    public bool IsMoving()
    {
        return rb.velocity != Vector2.zero;
    }
}
