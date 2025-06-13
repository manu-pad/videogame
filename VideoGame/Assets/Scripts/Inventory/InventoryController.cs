using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;

    void Start()
    {
        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
            if (i < itemPrefabs.Length)
            {
                GameObject item = Instantiate(itemPrefabs[i], slot.transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = item;
            }
        }

        inventoryPanel.SetActive(false); // come�a oculto
    }

    void Update()
    {
        bool isActive = VariableManager.Instance.GetVariable("inventoryActivate");
        inventoryPanel.SetActive(isActive);
        if(ItemDrag.booksPlaced == 3)
        {
            inventoryPanel.SetActive(false); // Esconde o invent�rio se 3 livros foram colocados
            VariableManager.Instance.SetVariable("inventoryActivate", false); // Desativa a vari�vel de ativa��o do invent�rio
        }
    }

    public bool AddItem(GameObject itemPrefab)
    {
        if (itemPrefab.GetComponent<RectTransform>() == null)
        {
            return false;
        }

        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.currentItem == null)
            {
                GameObject newItem = Instantiate(itemPrefab, slotTransform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = newItem;
                return true;
            }
        }

        Debug.Log("Inventory is full!");
        return false;
    }

    public void ClearInventory()
    {
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.currentItem != null)
            {
                Destroy(slot.currentItem);
                slot.currentItem = null;
            }
        }
    }

    public void ShowInventory()
{
    // S� mostra se o invent�rio foi desbloqueado
    if (VariableManager.Instance.GetVariable("inventoryActivate"))
    {
        inventoryPanel.SetActive(true);
    }
}

public void HideInventory()
{
    inventoryPanel.SetActive(false);
}

public bool IsInventoryFull()
{
    int filledSlots = 0;

    foreach (Transform slotTransform in inventoryPanel.transform)
    {
        Slot slot = slotTransform.GetComponent<Slot>();
        if (slot != null && slot.currentItem != null)
        {
            filledSlots++;
        }
    }

    return filledSlots >= slotCount;
}

}
