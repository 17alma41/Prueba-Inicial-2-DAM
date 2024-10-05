using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3;  // Vida m�xima del jugador
    private int currentHealth;

    [Header("UI")]
    public Slider healthBar;  // Referencia a la barra de vida (Slider)
    public Image fillImage;  // Imagen del Slider que cambia de color

    private void Start()
    {
        currentHealth = maxHealth;  // Inicializar la salud
        healthBar.maxValue = maxHealth;  // Asignar el valor m�ximo al slider
        healthBar.value = currentHealth;  // Establecer el valor inicial del slider
        UpdateHealthBarColor();
    }

    // Llamado cuando el jugador recibe da�o
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;  // Actualizar la barra de vida

        UpdateHealthBarColor();  // Actualizar el color de la barra de vida

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // M�todo para reiniciar la escena cuando el jugador muere
    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Cambiar el color de la barra de vida seg�n la salud restante
    private void UpdateHealthBarColor()
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        Color healthColor = Color.Lerp(Color.red, Color.green, healthPercentage);
        fillImage.color = healthColor;
    }

    // Si el jugador colisiona con un enemigo, toma da�o
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);  // Recibe 1 de da�o por cada colisi�n
        }
    }
}
