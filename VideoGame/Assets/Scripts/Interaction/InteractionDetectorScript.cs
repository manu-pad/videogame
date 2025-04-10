using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetectorScript : MonoBehaviour
{
    private IInteractable interactableInRange = null;
    public GameObject interactionIcon;


    void Start()
    {
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
        }
        else
        {
            Debug.Log("Nenhum objeto interativo detectado ou não pode interagir.");
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
    }

}
