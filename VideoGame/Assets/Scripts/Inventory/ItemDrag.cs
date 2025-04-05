using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent;
    private CanvasGroup canvasGroup;

    public float minDropDistance = 2f;
    public float maxDropDistance = 3f;

    public Collider2D tableArea; // Área da mesa


    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent; // Salva o pai original
        transform.SetParent(transform.root); // Move para cima na hierarquia
        canvasGroup.blocksRaycasts = false; // Desabilita raycasts para evitar interferências
        canvasGroup.alpha = 0.6f; // Deixa semi-transparente
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // Segue o mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // Reativa raycasts
        canvasGroup.alpha = 1f; // Torna visível novamente

        Slot dropSlot = eventData.pointerEnter?.GetComponent<Slot>(); // Pega o slot onde foi solto
        if(dropSlot == null)
        {
            GameObject dropItem = eventData.pointerEnter;
            if(dropItem != null)
            {
                dropSlot = dropItem.GetComponentInParent<Slot>();
            }
        }
        Slot originalSlot = originalParent.GetComponent<Slot>();

        if (dropSlot != null)
        {
            if (dropSlot.currentItem != null) // Se o slot já tem um item, troca os itens
            {
                dropSlot.currentItem.transform.SetParent(originalSlot.transform);
                originalSlot.currentItem = dropSlot.currentItem;
                dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            else
            {
                originalSlot.currentItem = null; // Remove do slot original
            }

            // Move o item para o novo slot
            transform.SetParent(dropSlot.transform);
            dropSlot.currentItem = gameObject;
        }
        else
        {
            if (!IsWithinInventory(eventData.position) && IsOverTable(eventData.position))
            {
                DropItem(originalSlot); // Se não estiver dentro do inventário, solta o item
            }
            else
            {
                transform.SetParent(originalParent); // Retorna ao slot original se não houver slot de drop
            }

        }
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // Reseta a posição

    }

    //acho que não está muito bem!! precisa verificar
    bool IsWithinInventory(Vector2 mousePosition)
    {
        if (originalParent == null || originalParent.parent == null)
        {
            Debug.LogError("originalParent ou seu parent está null! Retornando falso.");
            return false;
        }

        RectTransform inventoryRect = originalParent.parent.GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(inventoryRect, mousePosition);
    }

    bool IsOverTable(Vector2 mousePosition)
    {
        if (tableArea == null)
        {
            Debug.LogError("Table area não foi atribuída!");
            return false;
        }

        // Converte posição da tela para mundo
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPoint.z = 0; // Mantém no plano 2D

        return tableArea.bounds.Contains(worldPoint);
    }


    void DropItem(Slot originalSlot)
    {
        originalSlot.currentItem = null;

        if (tableArea == null)
        {
            Debug.LogError("tableArea não foi atribuído! O item não pode ser solto na mesa.");
            return;
        }

        // Posição do drop: exatamente no centro da mesa
        Vector2 dropPosition = tableArea.bounds.center;

        // Instanciar o item na posição da mesa
        Instantiate(gameObject, dropPosition, Quaternion.identity);

        // Destruir o item da UI
        Destroy(gameObject);
    }
}
