using UnityEngine;

[CreateAssetMenu(fileName = "InspectionTextsDatabase", menuName = "Game/Inspection Texts Database")]
public class InspectionTextsDatabase : ScriptableObject
{
    [TextArea(2, 5)]
    public string[] inspectionTexts;
}
