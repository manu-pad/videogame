using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    private InventoryController inventoryController;
    
    void Start()
    {
        inventoryController = FindObjectOfType<InventoryController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            GameObject item = collision.gameObject;
            if (item != null)
            {
                bool itemAdded = inventoryController.AddItem(collision.gameObject);

                if(itemAdded)
                {
                    Destroy(collision.gameObject); // Destroi o item ap�s adicion�-lo ao invent�rio
                }
            }
        }
    }
 
}
