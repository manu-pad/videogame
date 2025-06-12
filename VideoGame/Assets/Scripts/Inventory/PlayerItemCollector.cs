using UnityEngine;

//aqui fica no player
//gere a lógica para coletar objetos
//e também uma parte da lógica para inspecioná-los

public class PlayerItemCollector : MonoBehaviour
{
    private InventoryController inventoryController;
    private BookInspect bookInspect; // Para inspecionar o livro
    private GameObject currentItem; // Para armazenar o item com o qual o jogador está interagindo
    private bool isInspecting = false; // Para verificar se o jogador está inspecionando um item

    void Start()
    {
        inventoryController = FindObjectOfType<InventoryController>();
    }

    void Update()
    {
        if (inventoryController.IsInventoryFull())
        {
            Debug.Log("Inventário cheio!");
            return;
        }

        if (currentItem != null)
        {
            BookInspect inspect = currentItem.GetComponent<BookInspect>();
            bool isInspecting = inspect != null && inspect.IsInspectionActive();

            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inspect != null)
                {
                    if(isInspecting)
                    {
                        inspect.HideInspection();
                    }
                    else
                    {
                        inspect.ShowInspection();
                    }
                }
            }

            if (!isInspecting && Input.GetKeyDown(KeyCode.E))
            {
                var inspect2 = currentItem.GetComponent<BookInspect2>();
                if (inspect2 != null && !inspect2.CanInteract())
                    return;
                bool itemAdded = inventoryController.AddItem(currentItem);

                if (itemAdded)
                {
                    if (inspect != null)
                        inspect.HideInspection();

                    if (inspect2 != null)
                    {
                        inspect2.SetCollected(true); 
                    }

                    currentItem.SetActive(false); // Apenas uma vez!
                    currentItem = null; // Reseta o item atual
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item")) 
        {
            currentItem = collision.gameObject; // Armazena o item com o qual o jogador está interagindo
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            currentItem = null; // Reseta a variável currentItem quando o jogador sai da colisão
        }
    }
}
