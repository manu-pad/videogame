using UnityEngine;
using UnityEngine.UI; 


public class FrameInspect : MonoBehaviour, IInteractable
{
    public bool Inspect { get; private set; }
    public Image interactionImage;

    void Start()
    {
        if (interactionImage != null)
        {
            interactionImage.gameObject.SetActive(false); 
        }

    }

    void Update()
    {

    }


    public bool CanInteract()
    {
        return !Inspect;
    }

    public void Interact()
    {
        if (Inspect)
        {
            CloseFrame(); 
        }
        else
        {
            OpenFrame(); 
        }
    }

    private void OpenFrame()
    {
        Inspect = true; // Marca o frame como "aberto"
        if (interactionImage != null)
        {
            interactionImage.gameObject.SetActive(true); // Exibe a imagem de UI
        }
    }

    private void CloseFrame()
    {
        Inspect = false; // Marca o frame como "fechado"
        if (interactionImage != null)
        {
            interactionImage.gameObject.SetActive(false); // Oculta a imagem de UI
        }
    }
}
