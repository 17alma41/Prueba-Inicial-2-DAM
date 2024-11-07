using System.Collections;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] public float jumpForce = 10f;
    private bool canJump;

    [Header("Ground")]
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] float distanceToGround = 1f;
    private Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float friccion = 0.95f;

    [Header("Animaci?n")]
    private Animator animator;
    private bool facingRight = true;  // Control para saber si el personaje est? mirando a la derecha

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float movimientoHorizontal;
        movimientoHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeed;

        animator.SetFloat("Horizontal", Mathf.Abs(movimientoHorizontal));

        // Movimiento horizontal
        Vector2 movementInput = Vector2.zero;
        if (Input.GetKey(KeyCode.D))
            movementInput.x = 1;

        else if (Input.GetKey(KeyCode.A))
            movementInput.x = -1;

        // Chequear si est? en el suelo
        bool isGrounded = IsGrounded();

        // Si est? en el suelo, permitimos el salto
        if (isGrounded)
        {
            canJump = true;
            animator.SetBool("Jump", false);
        }

        // Manejo del salto
        if (Input.GetKeyDown(KeyCode.Space) && canJump && isGrounded)
        {
            Jump();
        }

        // Movimiento horizontal con Rigidbody
        if (movementInput.x != 0)
        {
            Move(movementInput.x);
        }
        else
        {
            // Aplicamos fricci?n para reducir la velocidad cuando no hay input
            rb.velocity = new Vector2(rb.velocity.x * friccion, rb.velocity.y);
        }

        // Controlar la direcci?n en la que mira el personaje
        if (movementInput.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (movementInput.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Move(float direction)
    {
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        canJump = false;
        animator.SetBool("Jump", true);
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(groundCheckPoint.position, Vector2.down, distanceToGround, GroundLayer);
    }

    // M?todo para invertir la direcci?n del personaje
    private void Flip()
    {
        facingRight = !facingRight;  // Cambiamos la direcci?n
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;  // Invertimos el eje X
        transform.localScale = scaler;
    }
}