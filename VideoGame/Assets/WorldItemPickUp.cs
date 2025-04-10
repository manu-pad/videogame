using UnityEngine;
using UnityEngine.EventSystems;

public class WorldItem : MonoBehaviour, IPointerClickHandler
{
    public GameObject inventoryItemPrefab;
    public Transform inventoryParent; // O container do invent�rio (slot vazio por exemplo)

    public void OnPointerClick(PointerEventData eventData)
    {
        InventoryController inventory = FindObjectOfType<InventoryController>();
        bool added = inventory.AddItem(inventoryItemPrefab);

        if (added)
        {
            Destroy(gameObject); // S� destr�i se adicionou ao invent�rio
        }
        else
        {
            Debug.Log("Invent�rio cheio!");
        }
    }
}
