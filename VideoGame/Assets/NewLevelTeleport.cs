using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewLevelTeleport : MonoBehaviour
{
    public Transform targetPosition;
    public float delay = 1f;
    public float extraDelayAfterTeleport = 0.5f;
    public CanvasGroup fadeCanvasGroup;
    public CanvasGroup levelNameCanvasGroup; // NOVO!
    public float fadeDuration = 1f;
    public float levelNameDisplayTime = 2f; // Tempo em que o nome do nível fica visível

    private bool isTeleporting = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTeleporting && other.CompareTag("Player"))
        {
            StartCoroutine(TeleportPlayerWithFade(other.gameObject));
        }
    }

    private IEnumerator TeleportPlayerWithFade(GameObject player)
    {
        isTeleporting = true;

        fadeCanvasGroup.gameObject.SetActive(true);

        // Fade In (tela preta)
        yield return StartCoroutine(FadeCanvasGroup(fadeCanvasGroup, 0f, 1f));
        PauseController.SetPause(true);

        // Espera um pouco com a tela preta
        yield return new WaitForSeconds(delay);

        // Teleporta o jogador
        player.transform.position = targetPosition.position;

        // Espera mais um pouco com a tela preta
        yield return new WaitForSeconds(extraDelayAfterTeleport);
        PauseController.SetPause(false);

        // Ativa e faz fade-in do nome do nível
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

        
        // Fade Out (tela preta)
        yield return StartCoroutine(FadeCanvasGroup(fadeCanvasGroup, 1f, 0f));
        fadeCanvasGroup.gameObject.SetActive(false);

        isTeleporting = false;
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
