using UnityEngine;
using UnityEngine.UI; // Para o uso de UI Button

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;
    public TabController tabController;
    public Button closeButton;  // Referência para o botão de fechar

    // Start é chamado antes do primeiro Update
    void Start()
    {
        menuCanvas.SetActive(false);

        // Verifica se o botão de fechar foi atribuído
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseMenu); // Associa o botão à função CloseMenu
        }
    }

    // Update é chamado a cada frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!menuCanvas.activeSelf && PauseController.IsGamePaused)
            {
                return;
            }

            bool isOpening = !menuCanvas.activeSelf;
            menuCanvas.SetActive(isOpening);
            PauseController.SetPause(isOpening);

            if (isOpening && tabController != null)
            {
                tabController.ActivateTab(0); // força a aba 0
            }
        }
    }

    // Função para fechar o menu
    public void CloseMenu()
    {
        menuCanvas.SetActive(false);
        PauseController.SetPause(false); // Despausa o jogo quando o menu for fechado
    }
}
