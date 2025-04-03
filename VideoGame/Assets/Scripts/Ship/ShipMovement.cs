using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class ShipMovement : MonoBehaviour
{
    private float shipSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * shipSpeed;
    }

    public void MoveShip(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
