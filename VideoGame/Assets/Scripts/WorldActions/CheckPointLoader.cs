using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CheckpointController : MonoBehaviour
{
    [Header("Pontos de entrada")]
    public Transform pontoA;
    public Transform pontoB;

    [Header("Imagens dos capítulos (devem estar no Canvas e começar desativadas)")]
    public Image capituloImgA;
    public Image capituloImgB;

    [Header("Fade Panel (CanvasGroup que controla o fade)")]
    public CanvasGroup fadeCanvasGroup;

    [Header("Tempos de fade")]
    public float tempoFadePanel = 4f;      // fade do painel (mais curto)
    public float tempoChapterImg = 8f;     // fade da imagem do capítulo (mais longo)

    private bool isLoading = false;

    private void Start()
    {
        fadeCanvasGroup.alpha = 0f;
        fadeCanvasGroup.gameObject.SetActive(false);
        capituloImgA.gameObject.SetActive(false);
        capituloImgB.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isLoading) return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        Collider2D playerCollider = player.GetComponent<Collider2D>();

        if (playerCollider.IsTouching(pontoA.GetComponent<Collider2D>()))
        {
            StartCoroutine(FadeNewLevelChapter(capituloImgA));
        }
        else if (playerCollider.IsTouching(pontoB.GetComponent<Collider2D>()))
        {
            StartCoroutine(FadeNewLevelChapter(capituloImgB));
        }
    }

    IEnumerator FadeNewLevelChapter(Image capituloImage)
    {
        isLoading = true;

        fadeCanvasGroup.gameObject.SetActive(true);
        capituloImgA.gameObject.SetActive(false);
        capituloImgB.gameObject.SetActive(false);
        capituloImage.gameObject.SetActive(true);

        fadeCanvasGroup.alpha = 0f;
        SetImageAlpha(capituloImage, 0f);

        float elapsed = 0f;

        // FADE IN simultâneo do painel e da imagem
        // painel sobe do 0 ao 1 em tempoFadePanel
        // imagem sobe do 0 ao 1 em tempoChapterImg (mais lento)
        while (elapsed < tempoChapterImg)
        {
            // Calcula alpha do painel (clamp para não passar de 1)
            float alphaPainel = Mathf.Clamp01(elapsed / tempoFadePanel);
            fadeCanvasGroup.alpha = alphaPainel;

            // Calcula alpha da imagem
            float alphaImagem = Mathf.Clamp01(elapsed / tempoChapterImg);
            SetImageAlpha(capituloImage, alphaImagem);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Garante alphas no máximo
        fadeCanvasGroup.alpha = 1f;
        SetImageAlpha(capituloImage, 1f);

        // Mantém tudo visível um tempo
        float holdTime = 2f;
        yield return new WaitForSeconds(holdTime);

        // FADE OUT simultâneo da imagem e do painel, tempoChapterImg
        elapsed = 0f;
        while (elapsed < tempoChapterImg)
        {
            float alpha = 1f - (elapsed / tempoChapterImg);
            fadeCanvasGroup.alpha = alpha;
            SetImageAlpha(capituloImage, alpha);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Finaliza invisível
        fadeCanvasGroup.alpha = 0f;
        SetImageAlpha(capituloImage, 0f);

        capituloImage.gameObject.SetActive(false);
        fadeCanvasGroup.gameObject.SetActive(false);

        isLoading = false;
    }

    private void SetImageAlpha(Image img, float alpha)
    {
        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }
}
