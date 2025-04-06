using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;
    public float acceleration = 10f;
    public float deceleration = 10f;

    [Header("Jump Settings")]
    public float jumpForce = 12f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public float doubleJumpCooldown = 0f;
    public bool hasDoubleJump = false;
    private int doubleJumpCount = 0;

    [Header("Ladder Settings")]
    public float climbSpeed = 4f;
    public LayerMask ladderLayer;
    public float ladderHorizontalSpeedMultiplier = 0.5f;

    [Header("Ladder Jump Settings")]
    public float ladderJumpForce = 14f;
    public float ladderJumpGravity = 0.5f;
    public float ladderJumpTransitionTime = 0.5f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float moveInput;
    private float verticalInput;
    private bool jumpPressed;
    private bool isSprinting;
    private bool isGrounded;
    private bool isClimbing;
    private bool wasClimbingLastFrame;
    private bool isLadderJumping;
    private float ladderJumpTimer;

    void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Input handling
        moveInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        jumpPressed = Input.GetButtonDown("Jump");
        isSprinting = Input.GetKey(KeyCode.LeftShift);

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Flip character
        if (moveInput > 0.01f)
            spriteRenderer.flipX = false;
        else if (moveInput < -0.01f)
            spriteRenderer.flipX = true;

        // Set animator parameters
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isClimbing", isClimbing);

        // New: set ClimbSpeed parameter only when climbing
        if (isClimbing)
        {
            animator.SetFloat("ClimbSpeed", Mathf.Abs(verticalInput));
        }
        else
        {
            animator.SetFloat("ClimbSpeed", 0f);
        }
    }

    void FixedUpdate()
    {
        wasClimbingLastFrame = isClimbing;
        CheckLadder();

        float currentGravity = 3f;

        if (isClimbing)
        {
            currentGravity = 0f;
        }
        else if (isLadderJumping)
        {
            ladderJumpTimer += Time.fixedDeltaTime;
            float t = Mathf.Clamp01(ladderJumpTimer / ladderJumpTransitionTime);
            currentGravity = Mathf.Lerp(ladderJumpGravity, 3f, t);

            if (t >= 1f)
                isLadderJumping = false;
        }

        rb.gravityScale = currentGravity;

        if (isClimbing)
        {
            float x = moveInput * moveSpeed * ladderHorizontalSpeedMultiplier;
            float y = verticalInput * climbSpeed;
            rb.linearVelocity = new Vector2(x, y);
        }
        else
        {
            float targetSpeed = moveInput * moveSpeed * (isSprinting ? sprintMultiplier : 1f);
            float speedDifference = targetSpeed - rb.linearVelocity.x;
            float accelRate = Mathf.Abs(targetSpeed) > 0.01f ? acceleration : deceleration;
            float movement = accelRate * speedDifference;
            rb.AddForce(Vector2.right * movement);
        }

        // Jump logic
        if (jumpPressed)
        {
            if (wasClimbingLastFrame)
            {
                ExitLadderJump();
            }
            else if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                doubleJumpCount = 0;
            }
            else if (hasDoubleJump && doubleJumpCount == 0 && doubleJumpCooldown <= 0f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                doubleJumpCount = 1;
                doubleJumpCooldown = 1f;
            }
        }

        if (doubleJumpCooldown > 0f)
        {
            doubleJumpCooldown -= Time.deltaTime;
        }
    }

    void CheckLadder()
    {
        Collider2D ladder = Physics2D.OverlapCircle(transform.position, 0.2f, ladderLayer);
        bool wantsToClimb = Mathf.Abs(verticalInput) > 0.1f || Mathf.Abs(moveInput) > 0.1f;

        if (ladder != null && wantsToClimb && !isLadderJumping)
        {
            isClimbing = true;
        }
        else
        {
            isClimbing = false;
        }
    }

    void ExitLadderJump()
    {
        isClimbing = false;
        isLadderJumping = true;
        ladderJumpTimer = 0f;
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, ladderJumpForce);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    public void TriggerOrbJump(float multiplier)
    {
        isClimbing = false;
        isLadderJumping = false;
        rb.gravityScale = 3f;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * multiplier);
    }
}
