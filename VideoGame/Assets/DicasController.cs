using UnityEngine;
using UnityEngine.UI;  // Importa o namespace para o componente Text
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DicasController : MonoBehaviour
{
    // Instância única do DicasController
    public static DicasController Instance;

    // Referência ao componente Text que você vai alterar
    public TMP_Text dicasText;

    // Garantindo que haja uma única instância do DicasController
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Não destruir o GameObject entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método para alterar o texto da dica
    public void SetDica(string mensagem)
    {
        if (dicasText != null)
        {
            dicasText.text = mensagem;  // Alterar o texto do componente Text
            Debug.Log(mensagem);
        }
    }
}
