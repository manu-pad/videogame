using System.Collections.Generic;
using UnityEngine;

public class DialogueFlagsManager : MonoBehaviour
{
    public static DialogueFlagsManager Instance;

    private Dictionary<string, bool> flags = new Dictionary<string, bool>();
    private Dictionary<string, IDialogueAction> flagActions = new Dictionary<string, IDialogueAction>();


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantém entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetFlag(string key, bool value)
    {
        flags[key] = value;

        if (value && flagActions.ContainsKey(key))
        {
            flagActions[key].Execute();
        }
    }

    public bool GetFlag(string key)
    {
        return flags.ContainsKey(key) && flags[key];
    }

    public void RegisterAction(string flag, IDialogueAction action)
    {
        if (!flagActions.ContainsKey(flag))
        {
            flagActions.Add(flag, action);
        }
    }
}
