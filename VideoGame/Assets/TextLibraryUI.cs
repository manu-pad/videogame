using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextLibraryUI : MonoBehaviour
{
    public GameObject libraryPanel;
    public GameObject buttonPrefab;
    public Transform buttonContainer;

    public GameObject libraryInspectionPanel; // painel que ser� mostrado ao clicar no bot�o
    public TextMeshProUGUI libraryInspectionText; // componente de texto dentro desse painel


    void Start()
    {
        if (libraryPanel != null)
            libraryPanel.SetActive(false);
        else
            Debug.LogWarning("LibraryPanel n�o atribu�do no Inspector.");
    }

    void Update()
    {
        
    }

    public void ShowLibrary()
    {
        PopulateLibrary();
    }

    void PopulateLibrary()
    {
        Debug.Log("Entrou em PopulateLibrary()");

        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        var readTexts = InspectionManager.Instance?.GetReadTexts();

        if (readTexts == null)
        {
            Debug.LogError("readTexts � null!");
            return;
        }

        Debug.Log($"N�mero de textos lidos: {readTexts.Count}");

        foreach (var entry in readTexts)
        {
            Debug.Log($"Criando bot�o para: {entry.title}");

            GameObject buttonObject = Instantiate(buttonPrefab, buttonContainer);

            // Achar o componente de texto e bot�o
            var textComponent = buttonObject.GetComponentInChildren<TextMeshProUGUI>();
            var buttonComponent = buttonObject.GetComponent<Button>(); // <- Esse � o bot�o do prefab

            if (textComponent == null || buttonComponent == null)
            {
                Debug.LogError("TextMeshProUGUI ou Button n�o encontrado no prefab!");
                continue;
            }

            textComponent.text = entry.title;
            Debug.Log($"Texto atribu�do ao bot�o: {entry.title}");  // Aqui � para verificar no console


            var capturedEntry = entry;

            // Aqui ligamos o clique ao m�todo que est� no TextLibraryUI
            buttonComponent.onClick.AddListener(() => ShowTextDetails(capturedEntry));
        }

    }


    void ShowTextDetails(InspectionTextsDatabase.InspectionTextEntry entry)
    {
        Debug.Log($"Mostrando detalhes para: {entry.title}");
        if (libraryInspectionPanel != null && libraryInspectionText != null)
        {
            libraryInspectionPanel.SetActive(true);
            libraryInspectionText.text = $"<b>{entry.title}</b>\n\n{entry.text}";
        }
        else
        {
            Debug.LogError("Painel de inspe��o da biblioteca ou texto n�o atribu�do!");
        }
    }

    public void HideLibraryInspection()
    {
        if (libraryInspectionPanel != null)
        {
            libraryInspectionPanel.SetActive(false);
        }
    }


    public void HideLibrary()
    {
        libraryPanel.SetActive(false);
    }
}
