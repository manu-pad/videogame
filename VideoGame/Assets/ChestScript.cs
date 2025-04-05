using UnityEngine;

public class ChestScript : MonoBehaviour, IInteractable
{
    public bool IsOpened { get; private set; }

    public string ChestID { get; private set; }
    public Sprite openedSprite;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool CanInteract()
    {
        return !IsOpened;
    }

    public void Interact()
    {
        if (!CanInteract())
        {
            return;
        }
        OpenChest(true);
    }

    private void OpenChest(bool opened)
    {
        if (IsOpened = opened)
        {
            GetComponent<SpriteRenderer>().sprite = openedSprite;
            SoundEffectManager.Play("Chest");
        }
    }
}
