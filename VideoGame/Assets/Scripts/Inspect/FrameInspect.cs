using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class FrameInspect : MonoBehaviour
{
    public GameObject interactionIcon; //ícone
    public GameObject inspectionUI; // referência ao UI de inspeção (painel ou canvas)
    private bool playerInRange = false;
    private bool isInspecting = false;

    void Start()
    {
        if (interactionIcon != null)
        {
            interactionIcon.gameObject.SetActive(false);
        }

        if (inspectionUI != null)
        {
            inspectionUI.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && Keyboard.current.iKey.wasPressedThisFrame)
        {
            ToggleInspect();
        }
    }

    private void ToggleInspect()
    {
        isInspecting = !isInspecting;

        if (inspectionUI != null)
        {
            inspectionUI.SetActive(isInspecting);
        }

        if (interactionIcon != null)
        {
            interactionIcon.gameObject.SetActive(!isInspecting);
        }

        if (isInspecting)
        {
            DicasController.Instance.SetDica(""); // Esconde a dica
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // certifique-se de que o jogador tem a tag "Player"
        {
            playerInRange = true;

            if (!isInspecting && interactionIcon != null)
                interactionIcon.gameObject.SetActive(true);

            DicasController.Instance.SetDica("Pressione [I] para inspecionar!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;

            if (interactionIcon != null)
                interactionIcon.gameObject.SetActive(false);

            DicasController.Instance.SetDica("");

            if (inspectionUI != null)
                inspectionUI.SetActive(false);

            isInspecting = false;
        }
    }
}
