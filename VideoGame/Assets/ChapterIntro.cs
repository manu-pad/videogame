using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChapterIntro : MonoBehaviour
{
    public CanvasGroup fadePanel;     // Referência ao FadePanel (imagem preta)
    public float fadePanelDuration = 3f;

    public CanvasGroup chapterCanvas; // Referência ao capítulo (título, por ex.)
    public float showTime = 2.5f;
    public float chapterFadeDuration = 5f;

    void Start()
    {
        StartCoroutine(PlayIntroSequence());
    }

    IEnumerator PlayIntroSequence()
    {
        // === 1. FadePanel (preto) desaparece ===
        fadePanel.alpha = 1f;
        float elapsedFade = 0f;
        while (elapsedFade < fadePanelDuration)
        {
            fadePanel.alpha = 1f - (elapsedFade / fadePanelDuration);
            elapsedFade += Time.deltaTime;
            yield return null;
        }
        fadePanel.alpha = 0f;
        fadePanel.gameObject.SetActive(false);

        // === 2. Chapter Intro aparece ===
        chapterCanvas.alpha = 1f;
        yield return new WaitForSeconds(showTime);

        // === 3. Chapter Intro some ===
        float elapsedChapter = 0f;
        while (elapsedChapter < chapterFadeDuration)
        {
            chapterCanvas.alpha = 1f - (elapsedChapter / chapterFadeDuration);
            elapsedChapter += Time.deltaTime;
            yield return null;
        }

        chapterCanvas.alpha = 0f;
        chapterCanvas.gameObject.SetActive(false);
    }
}
