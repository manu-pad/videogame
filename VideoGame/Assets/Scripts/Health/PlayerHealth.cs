using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 3f;
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
                Debug.Log("Vida j� est� no m�ximo, a flor n�o desaparece.");
            }
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
        currentHealth = Mathf.Max(0, currentHealth); // N�o deixar a vida ir abaixo de 0
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
        currentHealth = Mathf.Min(currentHealth, maxHealth); //nao passa do m�x
        healthUI.UpdateHearts(currentHealth);
    }

    //anima��o do dano + tempo que o player fica invulner�vel
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
