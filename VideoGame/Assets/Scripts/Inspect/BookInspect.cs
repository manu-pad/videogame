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
    public bool destroyAfterRead = false;


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
                    //atualiza a função da quest cada vez que o valor do livro atualiza
                    QuestsController.Instance?.UpdateQuestProgress("missionOne", booksInspectedCount1, 6);
                }
                wasRead = true;

               
            }

        }
    }

    public void HideInspection()
    {
        if (inspectionUI != null)
            inspectionUI.SetActive(false);
            PauseController.SetPause(false);
        //se o destruir estiver ativado na checkbox
        if (destroyAfterRead && wasRead)
        {
            Destroy(gameObject, 0.5f); // tempo opcional para dar tempo da UI desaparecer
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

}
