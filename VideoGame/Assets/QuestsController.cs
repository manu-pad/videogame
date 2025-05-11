
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class QuestsController : MonoBehaviour
{
    public static QuestsController Instance;
    public TMP_Text mainQuestText;

    private bool dicaAtualizada = false;

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

    void Update()
    {
        int booksInspected1 = BookInspect.booksInspectedCount1;
        GetQuest("missionOne", $"Encontre as anotações para abrir o portão e sair do Santuário\n {booksInspected1}/6 coletados");
    }

    public void SetQuest(string mensagem)
    {
        if (mainQuestText != null)
        {
            mainQuestText.text = mensagem;
        }
    }

    public void GetQuest(string variableName, string questText)
    {
        if (VariableManager.Instance.GetVariable(variableName))
        {
            if (!dicaAtualizada)
            {
                SetQuest(questText);
                DicasController.Instance.SetDica("Missão principal atualizada! Pressione TAB");
                dicaAtualizada = true;
            }
        }
    }
}


