using UnityEngine;
using UnityEngine.InputSystem;

public class InspectScript : MonoBehaviour
{
    public GameObject objectInspect;

    void Start()
    {
        objectInspect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            objectInspect.SetActive(!objectInspect.activeSelf);
        }
    }
}
