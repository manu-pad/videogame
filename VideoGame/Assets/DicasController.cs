using UnityEngine;
using UnityEngine.UI;  // Importa o namespace para o componente Text
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DicasController : MonoBehaviour
{
    // Inst�ncia �nica do DicasController
    public static DicasController Instance;

    // Refer�ncia ao componente Text que voc� vai alterar
    public TMP_Text dicasText;

    // Garantindo que haja uma �nica inst�ncia do DicasController
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // N�o destruir o GameObject entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // M�todo para alterar o texto da dica
    public void SetDica(string mensagem)
    {
        if (dicasText != null)
        {
            dicasText.text = mensagem;  // Alterar o texto do componente Text
            Debug.Log(mensagem);
        }
    }
}
