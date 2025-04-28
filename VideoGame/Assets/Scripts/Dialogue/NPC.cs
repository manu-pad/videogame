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
            return;
        }
        //clearout choices
        dialogueUI.ClearChoices();


        //check endDIalogueline
        if (dialogueData.endDialogueLines.Length > dialogueIndex && dialogueData.endDialogueLines[dialogueIndex])
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

        dialogueIndex++;

        if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            DisplayCurrentLine();
        }
        else
        {
            EndDialogue();
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


        //vai buscar as flags do NPCDialogue que passam no VariableManager
        if (dialogueData == null)
        {
            Debug.LogError("dialogueData está NULL!");
        }
        else if (VariableManager.Instance == null)
        {
            Debug.LogError("VariableManager.Instance está NULL!");
        }
        else if (dialogueData.setEndDialogueFlag && !string.IsNullOrEmpty(dialogueData.variableNameToSet))
        {
            VariableManager.Instance.SetVariable(dialogueData.variableNameToSet, true);
            Debug.Log($"Variável '{dialogueData.variableNameToSet}' foi ativada.");
        }
    }
}
