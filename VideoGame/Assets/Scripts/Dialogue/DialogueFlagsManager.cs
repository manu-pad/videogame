using System.Collections.Generic;
using UnityEngine;

public class DialogueFlagsManager : MonoBehaviour
{
    public static DialogueFlagsManager Instance;

    private Dictionary<string, bool> flags = new Dictionary<string, bool>();

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

    public void SetFlag(string flagName, bool value)
    {
        flags[flagName] = value;
    }

    public bool GetFlag(string flagName)
    {
        return flags.ContainsKey(flagName) && flags[flagName];
    }
}
