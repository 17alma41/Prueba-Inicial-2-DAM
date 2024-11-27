using UnityEngine;

public class HealingRune : MonoBehaviour
{
    public int healAmount = 1;  // Cantidad de curación que la runa proporciona

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);  // Curar al jugador
                Destroy(gameObject);  // Destruir la runa después de usarla
            }
        }
    }
}