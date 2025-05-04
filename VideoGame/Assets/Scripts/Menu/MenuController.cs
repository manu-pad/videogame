using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;
    public TabController tabController; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuCanvas.SetActive(false);
    }

    // Update is called once per frame
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
}
