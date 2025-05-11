using UnityEngine;
using TMPro; // para TextMeshPro
using System.Collections.Generic;

public class BookInspect : MonoBehaviour
{
    public GameObject inspectionUI; // painel de imagem (ativo/desativo aqui)
    public TextMeshProUGUI inspectionTextUI; // referência ao componente de texto dentro da imagem

    public InspectionTextsDatabase textDatabase; // ficheiro com todas as frases
    public int textIndex; // índice da frase a usar para este objeto
    private bool wasRead = false;

    public static int booksInspectedCount1 = 0;


    void Start()
    {
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
            }
            else
            {
                inspectionTextUI.text = "[Texto não encontrado]";
            }
            if (!wasRead)
            {
                booksInspectedCount1++;
                if (textIndex >= 0 && textIndex < textDatabase.inspectionTexts.Length)
                {
                    var entry = textDatabase.inspectionTexts[textIndex];
                    InspectionManager.Instance?.RegisterReadText(entry);
                }
                wasRead = true;
            }

        }
    }

    public void HideInspection()
    {
        if (inspectionUI != null)
            inspectionUI.SetActive(false);
    }

    public bool IsInspectionActive()
    {
        return inspectionUI != null && inspectionUI.activeSelf;
    }

    public bool WasRead()
    {
        return wasRead;
    }

}
