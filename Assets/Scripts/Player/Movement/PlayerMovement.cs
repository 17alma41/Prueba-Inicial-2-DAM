using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerStats stats;
    Rigidbody2D rb;

    [Header("Ground")]
    float timeWhenPressSpace;
    int remainingJumps;
    bool playerOnGround;
    bool wasOnGround = false;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheckPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        wasOnGround = playerOnGround;
        playerOnGround = EstaEnSuelo();

        MovementProcess();
        JumpProcess();
        Gravity();
    }

    void MovementProcess()
    {
        //Movimiento de teclas del player
        Vector2 movement = Vector2.zero;

        if (Input.GetKey(KeyCode.D))
            movement.x += 1;

        if (Input.GetKey(KeyCode.A))
            movement.x -= 1;

        if (Input.GetKey(KeyCode.RightArrow))
            movement.x += 1;

        if (Input.GetKey(KeyCode.LeftArrow))
            movement.x -= 1;

        //Aceleración y fricción del personaje
        if (playerOnGround)
        {
            if (movement != Vector2.zero)
            {
                rb.velocity += movement * stats.groundAcceleration * Time.deltaTime;
            }
            else
            {
                //Fricción en el suelo
                rb.velocity = new Vector2(rb.velocity.x / Mathf.Clamp(stats.groundFriction, 1, Mathf.Infinity), rb.velocity.y);
            }

            //Máxima velocidad del personaje horizontal en el suelo
            if (Mathf.Abs(rb.velocity.x) > stats.maxGroundHorizontalSpeed)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * stats.maxGroundHorizontalSpeed, rb.velocity.y);
            }
        }
        else if (!playerOnGround)
        {
            if (movement != Vector2.zero)
            {
                rb.velocity += movement * stats.airAcceleration * Time.deltaTime;
            }
            else
            {
                //Fricción en el aire
                rb.velocity = new Vector2(rb.velocity.x / Mathf.Clamp(stats.airFriction, 1, Mathf.Infinity), rb.velocity.y);
            }

            //Máxima velocidad del personaje horizontal en el aire
            if (Mathf.Abs(rb.velocity.x) > stats.maxAirHorizontalSpeed)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * stats.maxAirHorizontalSpeed, rb.velocity.y);
            }
        }

        //Limitar la velocidad de caída
        if (rb.velocity.y < -5) /////////////////////////////
        {
            rb.velocity = new Vector2(rb.velocity.x, -stats.maxFallSpeed);
        }
    }

    bool EstaEnSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            groundCheckPoint.position,
            groundCheckPoint.localScale,
            0f,
            Vector2.down,
            0.2f,
            groundLayer
        );


        return raycastHit.collider != null;
    }

    void JumpProcess()
    {
        if (EstaEnSuelo())
        {
            remainingJumps = stats.onAirJump;
        }

        if (Input.GetKeyDown(KeyCode.Space) && remainingJumps > 0)
        {
            float initialJumpForce = 3;
            rb.velocity = new Vector2(rb.velocity.x, initialJumpForce);

            remainingJumps--;

            timeWhenPressSpace = 0.0f;
        }

        if (Input.GetKey(KeyCode.Space) && remainingJumps > 0)
        {
            timeWhenPressSpace += Time.deltaTime;

            //Limitar salto cuando presiona el espacio
            if (stats.maxJumpPressTime >= timeWhenPressSpace)
            {
                //Le doy una fuerza al salto
                rb.velocity = new Vector2(rb.velocity.x, stats.jumpStregth);
            }
        }
    }

    void Gravity()
    {
        //Las partículas son para comprobar la función de gravedad que está realizando el personaje 

        if (rb.velocity.y > stats.yVelocityLowGravityThreshold)
        {
            rb.gravityScale = stats.defaultGravity;
            //particleColor.startColor = Color.white;
        }
        else if (rb.velocity.y < stats.yVelocityLowGravityThreshold && rb.velocity.y > -stats.yVelocityLowGravityThreshold)
        {
            rb.gravityScale = stats.lowGravity;
            //particleColor.startColor = Color.yellow;
        }
        else
        {
            rb.gravityScale = stats.fallingGravity;
            //particleColor.startColor = Color.green;
        }
    }

    // Aplicar knockback
    // recibir knockbak y aplicarlo en el movimiento
}
