using UnityEngine;
using UnityEngine.EventSystems;

//esse script fica nos objetos, que podem parar no invent�rio e serem arrastados de novo
public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private Transform originalParent;
    private CanvasGroup canvasGroup;

    public Collider2D tableArea; // �rea da mesa
    public static int booksPlaced = 0; // � compartilhada por todos

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent; // Salva o pai original
        transform.SetParent(transform.root); // Move para cima na hierarquia
        canvasGroup.blocksRaycasts = false; // Desabilita raycasts para evitar interfer�ncias
        canvasGroup.alpha = 0.6f; // Deixa semi-transparente
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // Segue o mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // Reativa raycasts
        canvasGroup.alpha = 1f; // Torna vis�vel novamente

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
            if (dropSlot.currentItem != null) // Se o slot j� tem um item, troca os itens
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
                DropItem(originalSlot); // Se n�o estiver dentro do invent�rio, solta o item
            }
            else
            {
                transform.SetParent(originalParent); // Retorna ao slot original se n�o houver slot de drop
            }

        }
        ResetSize();

        GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // Reseta a posi��o

    }

    void ResetSize()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.one; // Resetando a escala para 1
        rectTransform.sizeDelta = new Vector2(100, 100);  // Ajuste o tamanho conforme necess�rio
    }

    //acho que n�o est� muito bem!! precisa verificar
    bool IsWithinInventory(Vector2 mousePosition)
    {
        if (originalParent == null || originalParent.parent == null)
        {
            Debug.LogError("originalParent ou seu parent est� null! Retornando falso.");
            return false;
        }

        RectTransform inventoryRect = originalParent.parent.GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(inventoryRect, mousePosition);
    }

    bool IsOverTable(Vector2 mousePosition)
    {
        if (tableArea == null)
        {
            Debug.LogError("Table area n�o foi atribu�da!");
            return false;
        }

        // Converte posi��o da tela para mundo
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPoint.z = 0; // Mant�m no plano 2D

        return tableArea.bounds.Contains(worldPoint);
    }


    void DropItem(Slot originalSlot)
    {
        originalSlot.currentItem = null;

        if (tableArea == null)
        {
            Debug.LogError("tableArea n�o foi atribu�do! O item n�o pode ser solto na mesa.");
            return;
        }

        // Posi��o do drop: exatamente no centro da mesa
        Vector2 dropPosition = tableArea.bounds.center;

        // Instanciar o item na posi��o da mesa
        GameObject droppedItem = Instantiate(gameObject, dropPosition, Quaternion.identity);
        booksPlaced++;
        Debug.Log($"Livro colocado na mesa. {booksPlaced}");
        BookInspect2 inspect2 = droppedItem.GetComponent<BookInspect2>();
        if (inspect2 != null)
        {
            inspect2.SetInteractable(false); // Congela a intera��o
        }
        // Destruir o item da UI
        Destroy(gameObject);
    }

    //para mostrar o nome do livro
    public void OnPointerClick(PointerEventData eventData)
    {
        BookInspect2 book = GetComponent<BookInspect2>();
        if (book != null)
        {
            DicasController.Instance.SetDica(book.bookName);
        }
        else
        {
            Debug.LogWarning("BookInspect2 n�o encontrado no item!");
        }
    }

}
