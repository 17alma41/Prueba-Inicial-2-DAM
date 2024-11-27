using System.Collections;
using UnityEngine;


public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform firePoint;     // Punto desde donde se disparar� la bala
    public float bulletSpeed = 20f; // Velocidad de la bala

    [Header("Cooldown")]
    public float cooldown = 1f; // Duraci�n del cooldown
    private float elapsedCooldown = 0f; // El tiempo que transcurre desde que se lanza
    private bool isCooldown = false; // Comprueba si el cooldown est� activo

    [Header("Animaci�n")]
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Si se hace clic con el bot�n izquierdo del rat�n y no hay cooldown activo
        if (Input.GetMouseButtonDown(0) && !isCooldown)
        {
            Shoot();
            isCooldown = true;         // Activar cooldown
            elapsedCooldown = 0f;      // Reiniciar el contador de cooldown
        }

        // Incrementa el tiempo transcurrido durante el cooldown
        if (isCooldown)
        {
            elapsedCooldown += Time.deltaTime;

            // Si el tiempo transcurrido supera la duraci�n del cooldown, desactiva el cooldown
            if (elapsedCooldown >= cooldown)
            {
                isCooldown = false;
            }
        }
    }

    void Shoot()
    {
        // Activar la animaci�n de ataque
        animator.SetTrigger("Attack");

        // Crear la bala en el firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Obtener la direcci�n hacia el rat�n
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Asegurarse de que la bala se mantenga en el plano 2D
        Vector2 direction = (mousePosition - firePoint.position).normalized;

        // Aplicar movimiento a la bala
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;
        rb.gravityScale = 1; // A�adir gravedad realista

        // Destruir la bala despu�s de 2 segundos
        Destroy(bullet, 2f);
    }
}
