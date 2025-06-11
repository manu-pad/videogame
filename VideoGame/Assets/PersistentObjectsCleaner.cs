using UnityEngine;

public class PersistentObjectsCleaner : MonoBehaviour
{
    private static readonly string[] allowed = { "GameManager", "AudioManager" }; // objetos que *n�o* devem ser destru�dos

    void Awake()
    {
        foreach (var obj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (obj.scene.name == null || obj.scene.name == "")
            {
                if (!System.Array.Exists(allowed, name => obj.name.Contains(name)))
                {
                    Destroy(obj);
                }
            }
        }
    }
}
