using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;  // Velocidad del enemigo
    public float detectionRange = 5f;  // Distancia a la que el enemigo detecta al jugador

    [Header("Player Detection")]
    public Transform player;  // Referencia al jugador
    private bool playerInRange = false;

    [Header("Ground Check")]
    [SerializeField] Transform groundCheckPoint;  // Punto para verificar el suelo
    [SerializeField] LayerMask groundLayer;  // Capa del suelo
    [SerializeField] float groundCheckDistance = 1f;  // Distancia para verificar si hay suelo debajo

    private Rigidbody2D rb;
    private bool facingRight = true;  // Control para la direcci�n del enemigo
    public Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Verificamos la distancia al jugador
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }

        if (playerInRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y); // Forzamos el movimiento

            // Actualizamos la animaci�n "Walk" bas�ndonos en la velocidad en el eje X
            animator.SetFloat("Walk", Mathf.Abs(rb.velocity.x));

            // Voltear el enemigo hacia el jugador
            if ((direction.x > 0 && !facingRight) || (direction.x < 0 && facingRight))
            {
                Flip();
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y); // Detenemos el enemigo cuando el jugador est� fuera de rango
            animator.SetFloat("Walk", 0); // Detenemos la animaci�n de caminar
        }
    }

    private void FollowPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        // Voltear el enemigo hacia el jugador
        if ((direction.x > 0 && !facingRight) || (direction.x < 0 && facingRight))
        {
            Flip();
        }
    }

    private void StopMoving()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        animator.SetFloat("Walk", 0);  // Detenemos la animaci�n de caminar
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckDistance, groundLayer);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= 1;
        transform.localScale = scaler;
    }

    private void OnDrawGizmosSelected()
    {
        // Visualizar el rango de detecci�n en la escena
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    // Notificar el ataque
    // detectar cu�ndo ataca al jugador y envia la informacion necesaria para aplicar el knockback

    public void Knockback()
    {

    }
}
