using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    public GameObject cutsceneImage1;
    public GameObject cutsceneImage2;
    public GameObject backgroundCutScene;
    public float imageDisplayTime = 50f; // tempo em segundos

    public void OnStartClick()
    {
        StartCoroutine(PlayCutsceneAndStart());
    }

    IEnumerator PlayCutsceneAndStart()
    {
        backgroundCutScene.SetActive(true);

        // Mostrar imagem 1
        cutsceneImage1.SetActive(true);
        yield return new WaitForSeconds(imageDisplayTime);
        cutsceneImage1.SetActive(false);

        // Mostrar imagem 2
        cutsceneImage2.SetActive(true);
        yield return new WaitForSeconds(imageDisplayTime);
        cutsceneImage2.SetActive(false);

        // Carregar a cena
        SceneManager.LoadScene("Level2");
    }

    public void OnExitClick()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
        Application.Quit();
    }
}
