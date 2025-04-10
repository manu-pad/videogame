using UnityEngine;

public class BookInspect : MonoBehaviour
{
    public GameObject inspectionUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        if (inspectionUI != null)
            inspectionUI.SetActive(false);
    }

    public void ShowInspection()
    {
        if (inspectionUI != null)
            inspectionUI.SetActive(true);
    }

    public void HideInspection()
    {
        if (inspectionUI != null)
            inspectionUI.SetActive(false);
    }

    public bool IsInspectionActive()
    {
        return inspectionUI != null && inspectionUI.activeSelf;
    }
}
