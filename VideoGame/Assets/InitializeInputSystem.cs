using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InitializeInputSystem : MonoBehaviour
{
    private PlayerInput playerInput;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void DeactivateInputSystem()
    {
        playerInput.DeactivateInput();
        Debug.Log("PlayerInput desativado.");
    }


}
