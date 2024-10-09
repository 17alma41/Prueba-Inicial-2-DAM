using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBoss : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3;  // Vida m�xima del enemigo
    private int currentHealth;

    [Header("UI")]
    public Slider healthBar;  // Referencia a la barra de vida (Slider)
    public Image fillImage;  // Imagen del Slider que cambia de color

    [Header("Object to Move After Death")]
    public GameObject stone;  // Objeto que quieres mover (la piedra)
    public Vector3 targetPosition;  // Posici�n final a la que quieres mover la piedra
    public float moveSpeed = 2f;  // Velocidad a la que se mover� la piedra

    private bool bossDefeated = false;  // Bandera para verificar si el jefe ha sido derrotado

    private void Start()
    {
        currentHealth = maxHealth;  // Inicializar la salud
        healthBar.maxValue = maxHealth;  // Asignar el valor m�ximo al slider
        healthBar.value = currentHealth;  // Establecer el valor inicial del slider
        UpdateHealthBarColor();
    }

    // Llamado cuando el enemigo recibe da�o (por ejemplo, al ser disparado)
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;  // Actualizar la barra de vida

        UpdateHealthBarColor();  // Actualizar el color de la barra de vida

        if (currentHealth <= 0 && !bossDefeated)
        {
            Die();
        }
    }

    // M�todo para destruir al enemigo
    private void Die()
    {
        bossDefeated = true;  // Marcar al jefe como derrotado
        Destroy(gameObject);  // Destruir el objeto enemigo
        Debug.Log("Boss derrotado, iniciando el movimiento de la piedra.");

        // Iniciar el movimiento de la piedra
        StartCoroutine(MoveStone());
    }

    // Cambiar el color de la barra de vida seg�n la salud restante
    private void UpdateHealthBarColor()
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        Color healthColor = Color.Lerp(Color.red, Color.green, healthPercentage);
        fillImage.color = healthColor;
    }

    // Detectar colisiones con las balas del jugador
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);  // Recibe 1 de da�o por cada bala
            Destroy(collision.gameObject);  // Destruir la bala despu�s de colisionar
        }
    }

    // Coroutine para mover la piedra hacia la posici�n objetivo
    private IEnumerator MoveStone()
    {
        while (Vector3.Distance(stone.transform.position, targetPosition) > 0.1f)
        {
            Debug.Log("Moviendo la piedra...");  // Log para verificar si la piedra est� movi�ndose
            stone.transform.position = Vector3.MoveTowards(stone.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;  // Esperar un frame antes de continuar
        }

        Debug.Log("Piedra ha llegado a la posici�n objetivo.");
    }
}
