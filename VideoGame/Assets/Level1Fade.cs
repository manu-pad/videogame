using UnityEngine;
using System.Collections;

public class Level1Fade : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup; // Painel preto
    public CanvasGroup levelNameCanvasGroup; // Nome do nível

    public float fadeDuration = 1f;
    public float levelNameDisplayTime = 2f;
    public float delayBeforeFadeOut = 0.5f;
    public float extraDelayAfterTeleport = 0.5f;
    public float delay = 2f;


    private void Start()
    {
        fadeCanvasGroup.alpha = 1f;
        fadeCanvasGroup.gameObject.SetActive(true);
        StartCoroutine(FadeInScene());
    }

    private IEnumerator FadeInScene()
    {

        PauseController.SetPause(true);

        // Espera um pouco com a tela preta
        yield return new WaitForSeconds(delay);

        PauseController.SetPause(false);

        if (levelNameCanvasGroup != null)
        {
            levelNameCanvasGroup.gameObject.SetActive(true);
            yield return StartCoroutine(FadeCanvasGroup(levelNameCanvasGroup, 0f, 1f));

            // Espera com o nome visível
            yield return new WaitForSeconds(levelNameDisplayTime);

            // Fade-out do nome do nível
            yield return StartCoroutine(FadeCanvasGroup(levelNameCanvasGroup, 1f, 0f));
            levelNameCanvasGroup.gameObject.SetActive(false);
        }

        yield return StartCoroutine(FadeCanvasGroup(fadeCanvasGroup, 1f, 0f));
        fadeCanvasGroup.gameObject.SetActive(false);
        PauseController.SetPause(false);

    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha)
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timer / fadeDuration);
            canvasGroup.alpha = alpha;
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }
}
