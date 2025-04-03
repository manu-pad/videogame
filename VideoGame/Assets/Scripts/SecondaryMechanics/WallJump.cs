// WallJump.cs
using UnityEngine;

public class WallJump : MonoBehaviour
{
    public LayerMask wallLayer;
    private Rigidbody2D body;
    private Transform playerTransform;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerTransform = transform;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (onWall() && !isGrounded())
        {
            body.gravityScale = 0;
            body.linearVelocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 3;
        }

        if (Input.GetKey(KeyCode.W) && onWall())
        {
            WallJumpAction();
        }
    }

    private void WallJumpAction()
    {
        wallJumpCooldown = 0;
        body.linearVelocity = new Vector2(-Mathf.Sign(playerTransform.localScale.x) * 3, 6);
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(playerTransform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}
