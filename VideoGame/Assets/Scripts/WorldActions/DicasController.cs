using UnityEngine;
using System.Collections;
using TMPro;

public class DicasController : MonoBehaviour
{
    public static DicasController Instance;
    public TMP_Text dicasText;
    public float tempoDeExibicao = 3f;
    private Coroutine exibindoDicaCoroutine;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetDica(string mensagem)
    {
        if (exibindoDicaCoroutine != null)
        {
            StopCoroutine(exibindoDicaCoroutine);
        }
        exibindoDicaCoroutine = StartCoroutine(ExibirDicaTemporaria(mensagem));
    }

    private IEnumerator ExibirDicaTemporaria(string mensagem)
    {
        if (dicasText != null)
        {
            dicasText.text = mensagem;
        }

        yield return new WaitForSeconds(tempoDeExibicao);

        if (dicasText != null)
        {
            dicasText.text = "";
        }
    }
}
