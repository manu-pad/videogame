using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;              // referencia ao jogador
    public float speed = 2f;              // velocidade de movimento
    public float detectionRange = 5f;     // distancia para come�ar a perseguir
    public int lifePoints = 2;
    private int maxLifePoints;

    public bool shouldChase = true;

    private Rigidbody2D rb;
    private int currentHits = 0;

    //se o player spawnar, o enemy deve voltar a posição inicial
    private Vector2 initialPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;  // garante que a gravidade nao afeta
        initialPosition = transform.position;
        maxLifePoints = lifePoints;
    }

    void FixedUpdate()
    {
        if (player == null || !shouldChase)
            return;

        if (PauseController.IsGamePaused)
        {
            rb.linearVelocity = Vector2.zero;  
            return; 
        }


        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;  // para quando estiver fora do alcance
        }

    }

    public void EnemyTakeDamage()
    {
        currentHits++;
        Debug.Log($"Inimigo atingido! {currentHits}/{lifePoints} vezes.");

        if (currentHits >= lifePoints)
        {
            Destroy(gameObject);
        }
    }

    public void ResetEnemy()
    {
        transform.position = initialPosition; 
        lifePoints = maxLifePoints;  
        currentHits = 0; 
    }

}
