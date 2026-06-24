using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerController2D : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float maxSpeed = 6f;
    [SerializeField] private float acceleration = 15f;
    [SerializeField] private float deceleration = 20f;
    [SerializeField, Range(0f, 1f)] private float airControl = 0.8f;

    [Header("Salto")]
    [SerializeField] private float jumpHeight = 2.5f;
    [SerializeField] private float timeToJumpApex = 0.35f;
    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private float jumpBufferTime = 0.1f;

    [Header("Checagem do chao")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Áudio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpClip;


    private Animator anim;
    private Rigidbody2D rb;
    private float horizontalInput;
    private float coyoteCounter;
    private float jumpBufferCounter;
    private float jumpVelocity;
    private bool isGrounded;
    private bool inputLocked;
    private PlayerState currentState;

    public bool IsGrounded => isGrounded;
    public PlayerState CurrentState => currentState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        ConfigureJumpPhysics();
    }

    private void Update()
    {
        if (inputLocked)
        {
            horizontalInput = 0f;
            return;
        }

        horizontalInput = InputReader.Instance.MoveInput.x;

        // Update animator parameters
        anim.SetFloat("Speed", Mathf.Abs(horizontalInput));
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);

        UpdateJumpTimers();
        UpdateState();
    }

    private void FixedUpdate()
    {
        CheckGround();
        Move();
        TryJump();
    }

    public void SetInputLocked(bool locked)
    {
        inputLocked = locked;

        if (locked)
        {
            horizontalInput = 0f;
            coyoteCounter = 0f;
            jumpBufferCounter = 0f;
            currentState = PlayerState.Respawning;

            anim.SetTrigger("Die");
            
        }
        
        else
        {
        // Força o personagem a voltar para o estado Idle ao renascer
        
        anim.Play("PlayerIdle"); 
        }
    }

    private void ConfigureJumpPhysics()
    {
        float gravity = -(2f * jumpHeight) / (timeToJumpApex * timeToJumpApex);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        rb.gravityScale = Mathf.Abs(gravity) / Mathf.Abs(Physics2D.gravity.y);
    }

    private void UpdateJumpTimers()
    {
        if (isGrounded)
        {
            coyoteCounter = coyoteTime;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        if (InputReader.Instance.JumpPressed)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    private void Move()
    {
        float targetSpeed = horizontalInput * maxSpeed;
        float controlMultiplier = isGrounded ? 1f : airControl;
        float rate = Mathf.Abs(targetSpeed) > 0.01f ? acceleration : deceleration;
        float newVelocityX = Mathf.MoveTowards(rb.linearVelocity.x, targetSpeed, rate * controlMultiplier * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector2(newVelocityX, rb.linearVelocity.y);

        if (horizontalInput != 0f)
        {
            transform.localScale = new Vector3(Mathf.Sign(horizontalInput), 1f, 1f);
        }
    }

    private void TryJump()
    {
        if (jumpBufferCounter <= 0f || coyoteCounter <= 0f)
        {
            return;
        }

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVelocity);
        jumpBufferCounter = 0f;
        coyoteCounter = 0f;
        if (audioSource != null && jumpClip != null)
        {
            audioSource.PlayOneShot(jumpClip);
        }
    }

    private void CheckGround()
    {
        if (groundCheck == null)
        {
            isGrounded = false;
            return;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void UpdateState()
    {
        if (inputLocked)
        {
            currentState = PlayerState.Respawning;
        }
        else if (!isGrounded && rb.linearVelocity.y > 0.05f)
        {
            currentState = PlayerState.Jumping;
        }
        else if (!isGrounded && rb.linearVelocity.y < -0.05f)
        {
            currentState = PlayerState.Falling;
        }
        else if (Mathf.Abs(horizontalInput) > 0.01f)
        {
            currentState = PlayerState.Running;
        }
        else
        {
            currentState = PlayerState.Idle;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
        {
            return;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
