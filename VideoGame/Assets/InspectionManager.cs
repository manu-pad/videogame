using UnityEngine;
using System.Collections.Generic;

public class InspectionManager : MonoBehaviour
{
    public static InspectionManager Instance { get; private set; }

    private List<InspectionTextsDatabase.InspectionTextEntry> readTexts = new List<InspectionTextsDatabase.InspectionTextEntry>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Duplicate InspectionManager detected. Destroying.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Debug.Log("InspectionManager instanciado.");
    }

    public void RegisterReadText(InspectionTextsDatabase.InspectionTextEntry entry)
    {
        if (!readTexts.Contains(entry))
        {
            readTexts.Add(entry);
            Debug.Log($"Texto registrado no InspectionManager: {entry.title}");
        }
        else
        {
            Debug.Log($"Texto já estava registrado: {entry.title}");
        }
    }

    public List<InspectionTextsDatabase.InspectionTextEntry> GetReadTexts()
    {
        Debug.Log($"Total de textos registrados: {readTexts.Count}");
        return readTexts;
    }
}
