using UnityEngine;
using TMPro; // para TextMeshPro

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
                inspectionTextUI.text = textDatabase.inspectionTexts[textIndex];
            else
                inspectionTextUI.text = "[Texto não encontrado]";
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
