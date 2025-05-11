using UnityEngine;
using UnityEngine.UI; // Para o uso de UI Button

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;
    public TabController tabController;
    public Button closeButton;  // Refer�ncia para o bot�o de fechar

    // Start � chamado antes do primeiro Update
    void Start()
    {
        menuCanvas.SetActive(false);

        // Verifica se o bot�o de fechar foi atribu�do
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseMenu); // Associa o bot�o � fun��o CloseMenu
        }
    }

    // Update � chamado a cada frame
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
                tabController.ActivateTab(0); // for�a a aba 0
            }
        }
    }

    // Fun��o para fechar o menu
    public void CloseMenu()
    {
        menuCanvas.SetActive(false);
        PauseController.SetPause(false); // Despausa o jogo quando o menu for fechado
    }
}
