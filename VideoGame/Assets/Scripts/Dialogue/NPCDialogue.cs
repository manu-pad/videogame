using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "NPCDialogue")]

public class NPCDialogue : ScriptableObject
{
    public string npcName;
    public Sprite npcPortrait;
    public string[] dialogueLines;
    public bool[] autoProgressionLines;
    public bool[] endDialogueLines;
    public float autoProgressionDelay = 1.5f;
    public float typingSpeed = 0.05f;
    public AudioClip voiceSound;
    public float voicePitch = 1f;

    //flags
    public DialogueFlag[] lineFlags;

    //choices
    public DialogueChoice[] choices; //choices for the player
}

[System.Serializable]
public class DialogueChoice
{
    public int dialogueIndex; //dialogue lines where choices appear
    public string[] choices; //player response options
    public int[] nextDialogueIndexes; //indices of the next dialogues for each choice
}

[System.Serializable]
public class DialogueFlag
{
    public int dialogueLineIndex; // �ndice da linha do di�logo
    public string variableName;
    public bool value = true;
}

