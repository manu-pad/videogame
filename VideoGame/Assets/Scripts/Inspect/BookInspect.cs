using UnityEngine;
using TMPro;

public class BookInspect : MonoBehaviour
{
    public GameObject inspectionUI;
    public TextMeshProUGUI inspectionTextUI;
    public InspectionTextsDatabase textDatabase;
    public int textIndex;
    private bool wasRead = false;
    public bool destroyAfterRead = false;

    public static int booksInspectedCount1 = 0;

    private Vector2 initialPosition; // para resetar a posição
    private bool isCollected = false; // opcional, se quiser controlar isso também

    void Start()
    {
        initialPosition = transform.position;

        if (inspectionUI != null)
            inspectionUI.SetActive(false);
    }

    public void ShowInspection()
    {
        if (inspectionUI != null && textDatabase != null && inspectionTextUI != null)
        {
            inspectionUI.SetActive(true);

            if (textIndex >= 0 && textIndex < textDatabase.inspectionTexts.Length)
            {
                var entry = textDatabase.inspectionTexts[textIndex];
                string title = entry.title;
                string text = entry.text;
                inspectionTextUI.text = $"<b>{title}</b>\n\n{text}";
                PauseController.SetPause(true);
            }
            else
            {
                inspectionTextUI.text = "[Texto não encontrado]";
            }

            if (!wasRead)
            {
                if (textIndex >= 0 && textIndex < textDatabase.inspectionTexts.Length)
                {
                    var entry = textDatabase.inspectionTexts[textIndex];
                    InspectionManager.Instance?.RegisterReadText(entry);
                    booksInspectedCount1++;
                    QuestsController.Instance?.UpdateQuestProgress("missionOne", booksInspectedCount1, 6);
                }
                wasRead = true;
                isCollected = true; // se quiser controlar o estado de "coletado"
            }
        }
    }

    public void HideInspection()
    {
        if (inspectionUI != null)
            inspectionUI.SetActive(false);

        PauseController.SetPause(false);

        if (destroyAfterRead && wasRead)
        {
            gameObject.SetActive(false);
        }
    }

    public bool IsInspectionActive()
    {
        return inspectionUI != null && inspectionUI.activeSelf;
    }

    public bool WasRead()
    {
        return wasRead;
    }

    public void ResetBook()
    {
        if (isCollected)
        {
            transform.position = initialPosition; // reseta posição
            gameObject.SetActive(true); // reativa objeto
            wasRead = false; // permite leitura novamente
            isCollected = false; // reseta estado
        }
    }
}
