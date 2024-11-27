using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3;  // Vida máxima del jugador
    private int currentHealth;

    [Header("UI")]
    public Slider healthBar;  // Referencia a la barra de vida (Slider)
    public Image fillImage;  // Imagen del Slider que cambia de color

    [Header("Audio")]
    public AudioClip damageSound; // Clip de sonido para cuando recibe daño
    private AudioSource audioSource; // Componente AudioSource

    [Header("Particles")]
    public ParticleSystem damageParticles;  // Partículas de daño
    public ParticleSystem healParticles;    // Partículas de curación

    private void Start()
    {
        currentHealth = maxHealth;  // Inicializar la salud
        healthBar.maxValue = maxHealth;  // Asignar el valor máximo al slider
        healthBar.value = currentHealth;  // Establecer el valor inicial del slider
        UpdateHealthBarColor();

        // Obtener el componente AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    // Llamado cuando el jugador recibe daño
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;  // Actualizar la barra de vida
        UpdateHealthBarColor();  // Actualizar el color de la barra de vida

        // Reproducir el sonido de daño
        if (damageSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        // Reproducir las partículas de daño
        if (damageParticles != null)
        {
            damageParticles.Play();  // Reproducir partículas de daño
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Método para curar al jugador
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        // No exceder la vida máxima
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.value = currentHealth;  // Actualizar la barra de vida
        UpdateHealthBarColor();  // Actualizar el color de la barra de vida

        // Reproducir las partículas de curación
        if (healParticles != null)
        {
            healParticles.Play();  // Reproducir partículas de curación
        }
    }

    // Método para reiniciar la escena cuando el jugador muere
    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Cambiar el color de la barra de vida según la salud restante
    private void UpdateHealthBarColor()
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        Color healthColor = Color.Lerp(Color.red, Color.green, healthPercentage);
        fillImage.color = healthColor;
    }

    // Si el jugador colisiona con un enemigo, toma daño
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);  // Recibe 1 de daño por cada colisión
        }
    }
}
