using UnityEngine;
using TMPro; // para TextMeshPro
using System.Collections.Generic;

public class BookInspect : MonoBehaviour
{
    public GameObject inspectionUI; // painel de imagem (ativo/desativo aqui)
    public TextMeshProUGUI inspectionTextUI; // referência ao componente de texto dentro da imagem

    public InspectionTextsDatabase textDatabase; // ficheiro com todas as frases
    public int textIndex; // índice da frase a usar para este objeto

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

                // Adiciona o texto à lista de lidos se ainda não estiver na lista
                if (InspectionManager.Instance != null)
                {
                    InspectionManager.Instance.RegisterReadText(entry);
                }
                else
                {
                    Debug.LogWarning("InspectionManager não encontrado na cena!");
                }
            }
            else
            {
                inspectionTextUI.text = "[Texto não encontrado]";
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

}
