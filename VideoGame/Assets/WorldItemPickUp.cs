using UnityEngine;
using UnityEngine.EventSystems;

public class WorldItem : MonoBehaviour, IPointerClickHandler
{
    public GameObject inventoryItemPrefab;
    public Transform inventoryParent; // O container do inventário (slot vazio por exemplo)

    public void OnPointerClick(PointerEventData eventData)
    {
        InventoryController inventory = FindObjectOfType<InventoryController>();
        bool added = inventory.AddItem(inventoryItemPrefab);

        if (added)
        {
            Destroy(gameObject); // Só destrói se adicionou ao inventário
        }
        else
        {
            Debug.Log("Inventário cheio!");
        }
    }
}
