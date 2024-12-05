using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private PlayerMovement PlayerMovement;

    [Header("Health Settings")]
    public int maxHealth = 3;  // Vida m�xima del jugador
    private int currentHealth;

    [Header("UI")]
    public Slider healthBar;  // Referencia a la barra de vida (Slider)
    public Image fillImage;  // Imagen del Slider que cambia de color

    [Header("Audio")]
    public AudioClip damageSound; // Clip de sonido para cuando recibe da�o
    private AudioSource audioSource; // Componente AudioSource

    [Header("Particles")]
    public ParticleSystem damageParticles;  // Part�culas de da�o
    public ParticleSystem healParticles;    // Part�culas de curaci�n

    [SerializeField] float knockbackForce = 30f;

    private void Start()
    {
        currentHealth = maxHealth;  // Inicializar la salud
        healthBar.maxValue = maxHealth;  // Asignar el valor m�ximo al slider
        healthBar.value = currentHealth;  // Establecer el valor inicial del slider
        UpdateHealthBarColor();

        // Obtener el componente AudioSource
        audioSource = GetComponent<AudioSource>();

        PlayerMovement = GetComponent<PlayerMovement>();
    }

    // Llamado cuando el jugador recibe da�o
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;  // Actualizar la barra de vida
        UpdateHealthBarColor();  // Actualizar el color de la barra de vida

        // Reproducir el sonido de da�o
        if (damageSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        // Reproducir las part�culas de da�o
        if (damageParticles != null)
        {
            damageParticles.Play();  // Reproducir part�culas de da�o
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // M�todo para curar al jugador
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        // No exceder la vida m�xima
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.value = currentHealth;  // Actualizar la barra de vida
        UpdateHealthBarColor();  // Actualizar el color de la barra de vida

        // Reproducir las part�culas de curaci�n
        if (healParticles != null)
        {
            healParticles.Play();  // Reproducir part�culas de curaci�n
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
            GetComponent<Collider2D>().enabled = false;
            
            print(collision.contactCount); // Contador de puntos de contactos
            Vector3 contactPoint = collision.contacts[0].point; // TODO: Cojer el punto del enemigo
            Vector3 enemyPosition = collision.transform.position;

            // calcular el vector que va el punto de contacto hacia el centro del pj
            // el vector de A a B es B - A
            Vector3 knockbackDirection = transform.position - enemyPosition;

            knockbackDirection.Normalize();

            Debug.Log("Contactpoint: " + enemyPosition);
            Debug.Log("Knockback direction: " + knockbackDirection);

            // enviarle al movement ese vector como informaci�n de colisi�n
            PlayerMovement.Knockback(knockbackDirection, knockbackForce);

            TakeDamage(1);  // Recibe 1 de da�o por cada colisi�n
        }
    }
}
