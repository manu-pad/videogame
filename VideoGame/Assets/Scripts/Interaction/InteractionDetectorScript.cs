using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetectorScript : MonoBehaviour
{
    private IInteractable interactableInRange = null;
    public GameObject interactionIcon;
    private NPC npc;
    private BookInspect bookInspectInRange;


    void Start()
    {
        npc = FindObjectOfType<NPC>();
        interactionIcon.SetActive(false);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        //essa parte ta diferente
        if (context.performed && interactableInRange != null)
        {
            interactableInRange.Interact();
            if (interactableInRange.CanInteract())
            {
                interactionIcon.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            Debug.Log("Interativo detectado: " + interactable);
            interactionIcon.SetActive(true);
            DicasController.Instance.SetDica("Pressione [E] para interagir!");
            return;
        }

        if (collision.TryGetComponent(out BookInspect bookInspect)
                && !bookInspect.IsInspectionActive()
                && !bookInspect.WasRead())
        {
            bookInspectInRange = bookInspect;
            interactionIcon.SetActive(true);
            DicasController.Instance.SetDica("Pressione [I] para inspecionar!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            interactionIcon.SetActive(false);
            Debug.Log("Objeto interativo saiu da área.");
            DicasController.Instance.SetDica("");
        }

        if (collision.TryGetComponent(out BookInspect bookInspect) && bookInspect == bookInspectInRange)
        {
            bookInspectInRange = null;
            interactionIcon.SetActive(false);
            DicasController.Instance.SetDica("");
        }
    }

}
