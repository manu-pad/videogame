using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class QuestsController : MonoBehaviour
{
    public static QuestsController Instance;
    public TMP_Text mainQuestText;

    private Dictionary<string, int> previousCounts = new(); // progresso anterior
    private HashSet<string> dicasAtualizadas = new();       // se j� mostramos dica
    private HashSet<string> questsAtivadas = new();         // se a miss�o foi ativada

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
        CheckAndActivateQuest("missionOne", BookInspect.booksInspectedCount1, 6);
        //modelo novas miss�es
        // CheckAndActivateQuest("missionTwo", BookInspect.booksInspectedCount2, 8);
    }

    private void CheckAndActivateQuest(string variableName, int collected, int total)
    {
        if (VariableManager.Instance.GetVariable(variableName))
        {
            if (!questsAtivadas.Contains(variableName))
            {
                // miss�o foi ativada agora
                UpdateQuestProgress(variableName, collected, total);
                questsAtivadas.Add(variableName);
            }
        }
    }

    public void UpdateQuestProgress(string variableName, int collected, int total)
    {
        if (!VariableManager.Instance.GetVariable(variableName))
            return;

        if (!previousCounts.ContainsKey(variableName) || previousCounts[variableName] != collected)
        {
            SetQuest($"Encontre as anota��es para abrir o port�o e sair do Santu�rio\n {collected}/{total} coletados");
            previousCounts[variableName] = collected;

            if (!dicasAtualizadas.Contains(variableName))
            {
                DicasController.Instance.SetDica("Miss�o principal atualizada! Pressione TAB");
                dicasAtualizadas.Add(variableName);
            }
        }
    }

    public void SetQuest(string mensagem)
    {
        if (mainQuestText != null)
        {
            mainQuestText.text = mensagem;
        }
    }
}
