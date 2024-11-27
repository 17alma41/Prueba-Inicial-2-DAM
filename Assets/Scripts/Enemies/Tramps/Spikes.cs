using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float knockbackForce = 10f;  // Fuerza con la que los pinchos empujarán al jugador
    public int damageAmount = 1;        // Daño que infligen los pinchos (1 de daño en cada contacto)

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el objeto que colisiona es el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Reducir la vida del jugador
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);  // Llama a TakeDamage con un entero
            }

            // Aplicar un impulso hacia arriba
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, knockbackForce);  // Empujar hacia arriba
            }
        }
    }
}
