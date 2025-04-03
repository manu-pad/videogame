using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    bool isFacingRight = true;
    private SpriteRenderer spriteRenderer;
    private Animator animator;



    [Header ("Movement")]
    public float moveSpeed = 6f;

    float horizontalMovement;

    [Header ("Jumping")]
    public float jumpPower = 7f;
    public int maxJumps = 1; //apenas 1 pq acho que o 0 conta, por isso são 1 = 2 pulos
    int jumpsRemaining;

    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.75f, 0.05f);
    public LayerMask groundLayer;
    bool isGrounded;

    [Header("WallCheck")]
    public Transform wallCheckPos;
    public Vector2 wallCheckSize = new Vector2(0.05f, 0.75f );
    public LayerMask wallLayer;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 10f;
    public float fallSpeedMultiplier = 1f;

    [Header("WallMovement")]
    public float wallSlideSpeed = 2f;
    bool isWallSliding;

    [Header("WallJumping")]
    bool isWallJumping;
    float wallJumpDirection;
    float wallJumpTime = 0.5f;
    float wallJumpTimer;
    public Vector2 wallJumpPower = new Vector2(10f, 10f);



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        GroundCheck();
        Gravity();
        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);
            Flip();
        }

        animator.SetBool("run", horizontalMovement != 0);
        animator.SetBool("grounded", isGrounded);

    }

    public void Gravity()
    {
        if(rb.linearVelocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier; //player fall increasingly faster
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpsRemaining > 0)
        {
            if (context.performed)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                jumpsRemaining--;
                animator.SetTrigger("jump");
            }
            else if (context.canceled)//pular mais suave quando pressionar mais suave
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            }
        }

        //waljump
        if(context.performed && wallJumpTimer > 0)
        {
            isWallJumping = true;
            //jump away from wall
            rb.linearVelocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y);
            wallJumpTimer = 0;
            animator.SetTrigger("jump");


            //lógica para fazer o personagem virar-se
            isFacingRight = wallJumpDirection > 0;
            spriteRenderer.flipX = !isFacingRight;

            //inverte o wallCheckPos para o outro lado
            Vector3 wallCheckLocalPos = wallCheckPos.localPosition;
            wallCheckLocalPos.x = Mathf.Abs(wallCheckLocalPos.x) * (isFacingRight ? 1 : -1);
            wallCheckPos.localPosition = wallCheckLocalPos;

            //how long wall jump will last
            Invoke(nameof(CancelWallJump), wallJumpTime + 0.1f);
        }
        
    }

    private void GroundCheck()
    {
        if(Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private bool WallCheck()
    {
        return Physics2D.OverlapBox(wallCheckPos.position, wallCheckSize, 0, wallLayer);
    }

    private void Flip()
    {
        if (horizontalMovement < -0.01f)
        {
            isFacingRight = false;
        }
        else if (horizontalMovement > 0.01f)
        {
            isFacingRight = true;
        }
        spriteRenderer.flipX = !isFacingRight; // Inverte o sprite baseado na direção

        Vector3 wallCheckLocalPos = wallCheckPos.localPosition;
        wallCheckLocalPos.x = Mathf.Abs(wallCheckLocalPos.x) * (isFacingRight ? 1 : -1);
        wallCheckPos.localPosition = wallCheckLocalPos;
    }


    private void WallSlide()
    {
        if (!isGrounded & WallCheck() & horizontalMovement != 0)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -wallSlideSpeed));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallSliding = false;
            wallJumpDirection = isFacingRight ? -1 : 1;
            wallJumpTimer = wallJumpTime;

            CancelInvoke(nameof(CancelWallJump));
        }
        else if(wallJumpTimer > 0)
        {
            wallJumpTimer -= Time.deltaTime;
        }
    }

    private void CancelWallJump()
    {
        isWallJumping = false;
    }

    private void OnDrawGizmos()
    { 
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(wallCheckPos.position, wallCheckSize);
    }
}
