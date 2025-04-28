using System.Collections.Generic;
using UnityEngine;

public class VariableManager : MonoBehaviour
{
    public static VariableManager Instance;

    private Dictionary<string, bool> variables = new Dictionary<string, bool>();

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

    public void SetVariable(string name, bool value)
    {
        variables[name] = value;
    }

    public bool GetVariable(string name)
    {
        return variables.ContainsKey(name) && variables[name];
    }
}
