using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public float respawnDelay = 1f;  // Tempo de espera antes de respawnar
    private Enemy[] enemies;
    private Flower[] flowers;
    private Bird[] birds;  // Referência às ovelhas
    private NPC[] npcs;
    private BookInspect2[] books;
    private InventoryController inventory;

    void Start()
    {
        enemies = FindObjectsOfType<Enemy>();
        flowers = FindObjectsOfType<Flower>();
        birds = FindObjectsOfType<Bird>();
        npcs = FindObjectsOfType<NPC>();
        books = FindObjectsOfType<BookInspect2>();
        inventory = FindObjectOfType<InventoryController>();
    }

    public void Respawn()
    {
        //RespawnAtCheckpoint() //pra remover o delay
        Invoke("RespawnAtCheckpoint", respawnDelay);
    }

    private void RespawnAtCheckpoint()
    {
        transform.position = Checkpoint.lastCheckpointPosition;
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.RestoreFullHealth();
        }
        if (inventory != null)
        {
            inventory.ClearInventory();
        }
        foreach (var enemy in enemies)
        {
            enemy.ResetEnemy();
        }

        foreach (var flower in flowers)
        {
            flower.ResetFlower();
        }
        foreach (var bird in birds)
        {
            bird.ResetBird();
        }
        foreach (var npc in npcs)
        {
            npc.ResetDialogues();
        }
        foreach (var book in books)
        {
            book.ResetBook(); 
        }

        VariableManager.Instance.SetVariable("openGateOne", false);
        VariableManager.Instance.SetVariable("openGateTwo", false);
        VariableManager.Instance.SetVariable("openGateThree", false);
        VariableManager.Instance.SetVariable("openGateFour", false);
        VariableManager.Instance.SetVariable("openGateFive", false);
        VariableManager.Instance.SetVariable("openGateSix", false);
        VariableManager.Instance.SetVariable("finalGateOne", false);

        FindObjectOfType<WorldActionsController>()?.ResetGates();

        //resetação dos bookinspect1
        //resetar textos lidos
        InspectionManager.Instance?.ResetReadTexts();
        //resetar o UI
        var libraryUI = FindObjectOfType<TextLibraryUI>();
        if (libraryUI != null)
        {
            libraryUI.ShowLibrary(); // ou libraryUI.PopulateLibrary();
        }
        //resetar variável da missão
        BookInspect.booksInspectedCount1 = 0;
        QuestsController.Instance?.UpdateQuestProgress("missionOne", BookInspect.booksInspectedCount1, 7);
        BookInspect2.booksInspectedCount2 = 0;
        QuestsController.Instance?.UpdateQuestProgress("missionThree", BookInspect2.booksInspectedCount2, 3);
    }
}
