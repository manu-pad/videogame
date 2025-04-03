using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    public bool canInteract = true;

    // Método que é chamado quando o jogador interage com o objeto
    public void Interact()
    {
        // Aqui você pode definir a lógica da interação (ex: abrir uma porta, pegar um item, etc.)
        Debug.Log("Interagindo com o objeto!");
    }

    // Retorna se o objeto pode ser interagido
    public bool CanInteract()
    {
        return canInteract;
    }
}
