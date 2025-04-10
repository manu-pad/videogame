using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;              // refer�ncia ao jogador
    public float speed = 2f;              // velocidade de movimento
    public float detectionRange = 5f;     // dist�ncia para come�ar a perseguir

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;  // garante que a gravidade n�o afeta
    }

    void FixedUpdate()
    {
        if (player == null)
            return;

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

    
}
