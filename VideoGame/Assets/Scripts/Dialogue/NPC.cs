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

    public bool finalDialogueEnd;

    void Start()
    {
        dialogueUI = DialogueManager.Instance;
    }

    public bool CanInteract()
    {
        return !isDialogueActive && !finalDialogueEnd;
    }

    public void Interact()
    {
        // PauseController.IsGamePaused && !isDialogueActive
        if (dialogueData == null) return;

        if (finalDialogueEnd == true)
        {
            NextLine();
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
        PauseController.SetPause(true);

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

        // Clear out choices
        dialogueUI.ClearChoices();

        // Check endDialogueLine
        if (dialogueData.endDialogueLines.Length > dialogueIndex && dialogueData.endDialogueLines[dialogueIndex])
        {
            EndDialogue();
            return;
        }

        // Check if there are choices
        foreach (DialogueChoice dialogueChoice in dialogueData.choices)
        {
            if (dialogueChoice.dialogueIndex == dialogueIndex)
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

        // Verifica se esta linha tem alguma flag associada
        if (dialogueData.lineFlags != null)
        {
            foreach (var flag in dialogueData.lineFlags)
            {
                if (flag.dialogueLineIndex == dialogueIndex && !string.IsNullOrEmpty(flag.variableName))
                {
                    VariableManager.Instance.SetVariable(flag.variableName, flag.value);
                    Debug.Log($"Flag '{flag.variableName}' ativada na linha {dialogueIndex}.");
                    finalDialogueEnd = true;
                }
            }
        }

        StartCoroutine(TypeLine());
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueUI.SetDialogueText("");
        dialogueUI.ShowDialogueUI(false);
        PauseController.SetPause(false);

        if (inventoryController != null)
        {
            inventoryController.inventoryPanel.SetActive(true);
        }

    }
    public void ResetDialogues()
    {
        finalDialogueEnd = false;
    }



}
