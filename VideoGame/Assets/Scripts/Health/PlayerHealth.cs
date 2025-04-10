using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    //public healthUI;

    void Start()
    {
       currentHealth = maxHealth;
        //healthUI.setMaxHearts(MaxHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            TakeDamage(1);
            Debug.Log("Menos 1 de vida");
        }
    }


    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //healthUI.setHearts(currentHealth);
        if (currentHealth <= 0)
        {
            //player dead gameover animation
        }
    }
}
