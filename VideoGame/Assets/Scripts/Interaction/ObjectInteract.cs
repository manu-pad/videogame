using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    public bool canInteract = true;

    // M�todo que � chamado quando o jogador interage com o objeto
    public void Interact()
    {
        // Aqui voc� pode definir a l�gica da intera��o (ex: abrir uma porta, pegar um item, etc.)
        Debug.Log("Interagindo com o objeto!");
    }

    // Retorna se o objeto pode ser interagido
    public bool CanInteract()
    {
        return canInteract;
    }
}
