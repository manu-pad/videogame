using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 10f;
    private float currentHealth;
    private bool invulnerable = false;

    public HealthUI healthUI;

    private SpriteRenderer spriteRenderer;
    private PlayerRespawn playerRespawn;


    void Start()
    {
        currentHealth = maxHealth;
        healthUI.SetMaxHearts(Mathf.CeilToInt(maxHealth));

        playerRespawn = GetComponent<PlayerRespawn>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }


    void Update()
    {
        //para testes, remover depois
        if (Input.GetKeyDown(KeyCode.Y))
        {
            TakeDamage(1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Flower"))
        {
            if (currentHealth < maxHealth)
            {
                GainHealth(0.5f);
                Destroy(collider.gameObject);
                Debug.Log("Vida aumentada para: " + currentHealth);
            }
            else
            {
                Debug.Log("Vida já está no máximo, a flor não desaparece.");
            }
        }
        if (collider.transform.CompareTag("Spike"))
        {
            TakeDamage(1f);
            Debug.Log("Vidas restantes: " + currentHealth);
        }
        if (collider.transform.CompareTag("Lava"))
        {
            TakeDamage(1f);
            Debug.Log("Vidas restantes: " + currentHealth);
        }
        if (collider.transform.CompareTag("Water"))
        {
            TakeDamage(1f);
            Debug.Log("Vidas restantes: " + currentHealth);
        }
        if (collider.transform.CompareTag("FallingBlock"))
        {
            TakeDamage(1f);
            Debug.Log("Vidas restantes: " + currentHealth);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            TakeDamage(1f);
            Debug.Log("Vidas  restantes: " + currentHealth);
        }
    }

    private void TakeDamage(float damage)
    {
        if (invulnerable)
            return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth); // Não deixar a vida ir abaixo de 0
        healthUI.UpdateHearts(currentHealth);
        StartCoroutine(FlashRedAndInvulnerable());


        if (currentHealth <= 0)
        {
            playerRespawn.Respawn();
        }
    }

    public void RestoreFullHealth()
    {
        currentHealth = maxHealth;
        healthUI.SetMaxHearts(Mathf.CeilToInt(maxHealth));
    }


    private void GainHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth); //nao passa do máx
        healthUI.UpdateHearts(currentHealth);
    }

    //animação do dano + tempo que o player fica invulnerável
    private IEnumerator FlashRedAndInvulnerable()
    {
        Color originalColor = spriteRenderer.color;
        invulnerable = true; 

        float duration = 0.2f;
        float interval = 0.05f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(interval);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(interval);
            elapsed += interval * 3;
        }

        spriteRenderer.color = originalColor;
        invulnerable = false;
    }

}
