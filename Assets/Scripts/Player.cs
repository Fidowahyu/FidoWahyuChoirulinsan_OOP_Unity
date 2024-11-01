using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    private PlayerMovement playerMovement;
    private Animator animator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GameObject.Find("Engine/EngineEffect")?.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        playerMovement.Move();
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        bool isMoving = playerMovement.IsMoving();
        animator.SetBool("IsMoving", isMoving);
    }

}
