using UnityEngine;

public class BookInspect2 : MonoBehaviour, IInteractable
{
    [Header("Informações do Livro")]
    public string bookName; 

    private bool isInteractable = true;
    private Vector2 initialPosition; 
    private bool isCollected = false;

    void Start()
    {
        initialPosition = transform.position;
    }

    public void Interact()
    {
        if (!CanInteract()) return;

        Debug.Log($"Você interagiu com o livro: {bookName}");
    }

    public bool CanInteract()
    {
        return isInteractable;
    }

    public void SetInteractable(bool value)
    {
        isInteractable = value;
    }

    public void ResetBook()
    {
        if (isCollected)
        {
            transform.position = initialPosition; // Restaura a posição inicial
            gameObject.SetActive(true); // Reativa o livro
            isCollected = false; // Reseta o estado de coletado
        }
    }

    public void SetCollected(bool value)
    {
        isCollected = value;
    }

}
