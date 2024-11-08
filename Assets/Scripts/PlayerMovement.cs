using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 maxSpeed = new Vector2(5f, 5f); // Pastikan ada nilai default
    [SerializeField] private Vector2 timeToFullSpeed = new Vector2(1f, 1f); // Nilai default untuk mencegah pembagian oleh nol
    [SerializeField] private Vector2 timeToStop = new Vector2(1f, 1f); // Nilai default untuk mencegah pembagian oleh nol
    [SerializeField] private Vector2 stopClamp = new Vector2(0.5f, 0.5f); // Pastikan ada nilai untuk batas kecepatan saat berhenti
    
    private Vector2 moveVelocity;
    private Vector2 moveFriction;
    private Rigidbody2D rb;
    private Camera mainCamera;
    private Vector2 cameraBounds;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;

        // Pastikan Rigidbody2D ada pada GameObject
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on the player object. Please add a Rigidbody2D component.");
            return;
        }

        // Inisialisasi moveVelocity dan moveFriction
        moveVelocity = new Vector2(
            maxSpeed.x / Mathf.Max(timeToFullSpeed.x, 0.01f),
            maxSpeed.y / Mathf.Max(timeToFullSpeed.y, 0.01f)
        );
        
        moveFriction = new Vector2(
            -maxSpeed.x / Mathf.Max(timeToStop.x, 0.01f),
            -maxSpeed.y / Mathf.Max(timeToStop.y, 0.01f)
        );

        // Hitung batas kamera berdasarkan orthographicSize (untuk kamera 2D)
        if (mainCamera != null)
        {
            float cameraHeight = mainCamera.orthographicSize * 2;
            float cameraWidth = cameraHeight * mainCamera.aspect;
            cameraBounds = new Vector2(cameraWidth / 2, cameraHeight / 2);
        }
    }

    void FixedUpdate()
    {
        if (rb != null) // Pastikan rb tidak null
        {
            Move();
            ClampPosition();
        }
    }

    public void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        Vector2 moveDirection = new Vector2(inputX, inputY).normalized;

        if (moveDirection != Vector2.zero)
        {
            rb.velocity = Vector2.ClampMagnitude(
                rb.velocity + moveVelocity * moveDirection * Time.fixedDeltaTime, 
                maxSpeed.magnitude
            );
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

    private void ClampPosition()
    {
        // Dapatkan posisi pemain saat ini
        Vector3 pos = rb.position;

        // Batasi posisi berdasarkan batas kamera
        pos.x = Mathf.Clamp(pos.x, -cameraBounds.x, cameraBounds.x);
        pos.y = Mathf.Clamp(pos.y, -cameraBounds.y, cameraBounds.y);

        // Set posisi baru
        rb.position = pos;
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0.1f;
    }
}
