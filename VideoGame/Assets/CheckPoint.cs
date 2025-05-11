using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Vector2 lastCheckpointPosition; // �ltima posi��o de checkpoint ativado

    private void Start()
    {
        // Se este for o primeiro checkpoint a ser ativado no jogo, salva sua posi��o inicial
        if (lastCheckpointPosition == Vector2.zero)
        {
            lastCheckpointPosition = transform.position;
            Debug.Log("Checkpoint inicial: " + lastCheckpointPosition);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            lastCheckpointPosition = transform.position;
            Debug.Log("Novo checkpoint ativado em: " + lastCheckpointPosition);
        }
    }
}
