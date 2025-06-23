using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenuController : MonoBehaviour
{
    public GameObject cutsceneImage1;
    public GameObject cutsceneImage2;
    public GameObject backgroundCutScene;
    public TextMeshProUGUI continueText; // Texto "Pressione Enter para continuar"

    public float delayBeforeShowingText = 2f;

    public void OnStartClick()
    {
        StartCoroutine(PlayCutsceneAndStart());
    }

    IEnumerator PlayCutsceneAndStart()
    {
        backgroundCutScene.SetActive(true);

        // Mostrar imagem 1
        cutsceneImage1.SetActive(true);
        yield return new WaitForSeconds(delayBeforeShowingText);
        continueText.gameObject.SetActive(true);
        yield return WaitForEnter();
        continueText.gameObject.SetActive(false);
        cutsceneImage1.SetActive(false);

        // Mostrar imagem 2
        cutsceneImage2.SetActive(true);
        yield return new WaitForSeconds(delayBeforeShowingText);
        continueText.gameObject.SetActive(true);
        yield return WaitForEnter();
        continueText.gameObject.SetActive(false);
        cutsceneImage2.SetActive(false);
        yield return WaitForEnter();

        // Carregar a próxima cena
        SceneManager.LoadScene("Level2");
    }

    IEnumerator WaitForEnter()
    {
        while (!Input.GetKeyDown(KeyCode.Return) && !Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            yield return null;
        }
    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
