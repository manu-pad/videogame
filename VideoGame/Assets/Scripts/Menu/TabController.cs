using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public Button[] tabButtons;
    public GameObject[] pages;

    void Start()
    {
        for (int i = 0; i < tabButtons.Length; i++)
        {
            int index = i; // Evita problemas de referência em lambda
            tabButtons[i].onClick.AddListener(() => ActivateTab(index));
        }

        ActivateTab(0); // Ativa a primeira aba
    }


    public void ActivateTab(int tabNo)
    {

        if (tabNo < 0 || tabNo >= pages.Length || tabNo >= tabButtons.Length)
        {
            Debug.LogError("Índice de aba fora do intervalo!");
            return;
        }

        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            SetButtonColor(tabButtons[i], Color.grey);
        }

        pages[tabNo].SetActive(true);
        SetButtonColor(tabButtons[tabNo], Color.white);
    }

    private void SetButtonColor(Button button, Color color)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = color;
        button.colors = colors;
    }
}
