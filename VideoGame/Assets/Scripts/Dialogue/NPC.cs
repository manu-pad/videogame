using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;
    public InventoryController inventoryController; 


    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

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

        nameText.SetText(dialogueData.npcName);
        portraitImage.sprite = dialogueData.npcPortrait;

        dialoguePanel.SetActive(true);
        //PauseController.SetPause(true);

        if (inventoryController != null)
        {
            inventoryController.inventoryPanel.SetActive(false);
        }

        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();

            // Debugando o valor de dialogueIndex
            Debug.Log("Fim do di�logo. �ndice atual: " + dialogueIndex);

            // Ativar vari�veis com base nos flags de di�logo
            foreach (var flag in dialogueData.dialogueFlags)
            {
                Debug.Log($"Verificando flag para a linha {flag.lineIndex} (Vari�vel: {flag.variableName})");

                // Verifique se o �ndice da linha corresponde ao �ndice do di�logo
                if (flag.lineIndex == dialogueIndex)
                {
                    // Ativa a flag no DialogueFlagsManager
                    DialogueFlagsManager.Instance.SetFlag(flag.variableName, true);
                    bool flagValue = DialogueFlagsManager.Instance.GetFlag(flag.variableName);
                    Debug.Log("Vari�vel ativada: " + flag.variableName);
                }
            }
        }
    }



    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");

        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        if (dialogueData.autoProgressionLines.Length > dialogueIndex && dialogueData.autoProgressionLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressionDelay);
            NextLine();
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);
        //PauseController.SetPause(false);

        if (inventoryController != null)
        {
            inventoryController.inventoryPanel.SetActive(true);
        }
    }
}
