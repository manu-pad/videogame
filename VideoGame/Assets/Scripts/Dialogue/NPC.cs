using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogueData;
    private DialogueManager dialogueUI;

    public InventoryController inventoryController; 


    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    void Start()
    {
        dialogueUI = DialogueManager.Instance;
    }

    public bool CanInteract()
    {
        return !isDialogueActive;
    }


    public void Interact()
    {
        //PauseController.IsGamePaused && !isDialogueActive
        if (dialogueData == null)
        {
            return;
        }

        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        dialogueUI.SetNPCInfo(dialogueData.npcName, dialogueData.npcPortrait);

        dialogueUI.ShowDialogueUI(true);

        //PauseController.SetPause(true);

        if (inventoryController != null)
        {
            inventoryController.inventoryPanel.SetActive(false);
        }

        DisplayCurrentLine();
    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueUI.SetDialogueText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        //clearout choices
        dialogueUI.ClearChoices();

        //check endDIalogueline
        if(dialogueData.endDialogueLines.Length > dialogueIndex && dialogueData.endDialogueLines[dialogueIndex])
        {
            EndDialogue();
            return;
        }

        //check if there are choices
        foreach(DialogueChoice dialogueChoice in dialogueData.choices)
        {
            if(dialogueChoice.dialogueIndex == dialogueIndex)
            {
                DisplayChoices(dialogueChoice);
                return;
            }
        }

        if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            DisplayCurrentLine();
        }
        else
        {
            EndDialogue();

            // Debugando o valor de dialogueIndex
            Debug.Log("Fim do diálogo. Índice atual: " + dialogueIndex);

            // Ativar variáveis com base nos flags de diálogo
            foreach (var flag in dialogueData.dialogueFlags)
            {
                Debug.Log($"Verificando flag para a linha {flag.lineIndex} (Variável: {flag.variableName})");

                // Verifique se o índice da linha corresponde ao índice do diálogo
                if (flag.lineIndex == dialogueIndex)
                {
                    // Ativa a flag no DialogueFlagsManager
                    DialogueFlagsManager.Instance.SetFlag(flag.variableName, true);
                    bool flagValue = DialogueFlagsManager.Instance.GetFlag(flag.variableName);
                    Debug.Log("Variável ativada: " + flag.variableName);
                }
            }
        }
    }



    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueUI.SetDialogueText("");

        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueUI.SetDialogueText(dialogueUI.dialogueText.text += letter);

            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        if (dialogueData.autoProgressionLines.Length > dialogueIndex && dialogueData.autoProgressionLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressionDelay);
            NextLine();
        }
    }

    void DisplayChoices(DialogueChoice choice)
    {
        for (int i = 0; i < choice.choices.Length; i++)
        {
            int nextIndex = choice.nextDialogueIndexes[i];
            dialogueUI.CreateChoiceButton(choice.choices[i], () => ChooseOption(nextIndex));
        }
    }

    void ChooseOption(int nextIndex)
    {
        dialogueIndex = nextIndex;
        dialogueUI.ClearChoices();
        DisplayCurrentLine();
    }

    void DisplayCurrentLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeLine());
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueUI.SetDialogueText("");
        dialogueUI.ShowDialogueUI(false);
        //PauseController.SetPause(false);

        if (inventoryController != null)
        {
            inventoryController.inventoryPanel.SetActive(true);
        }
    }
}
