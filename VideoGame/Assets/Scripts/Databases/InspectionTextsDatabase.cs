using UnityEngine;

[CreateAssetMenu(fileName = "InspectionTextsDatabase", menuName = "Game/Inspection Texts Database")]
public class InspectionTextsDatabase : ScriptableObject
{
    [System.Serializable]
    public class InspectionTextEntry
    {
        public string title;

        [TextArea(2, 5)]
        public string text;
    }

    public InspectionTextEntry[] inspectionTexts;

}
