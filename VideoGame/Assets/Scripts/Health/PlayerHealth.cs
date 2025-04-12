using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;


    public HealthUI healthUI;

    void Start()
    {
       currentHealth = maxHealth;
        healthUI.SetMaxHearts(maxHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            TakeDamage(1);
            Debug.Log("Vidas restantes: " + currentHealth);
        }
    }


    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthUI.UpdateHearts(currentHealth);
        if (currentHealth <= 0)
        {
            //player dead gameover animation
        }
    }
}
