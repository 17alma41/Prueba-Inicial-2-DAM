using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMovement : MonoBehaviour
{
    [Header("Posicionamiento del Arma")]
    public Transform player;               // Asigna el transform del jugador en el Inspector
    public float distanceFromPlayer = 1f;  // Distancia deseada desde el jugador
    public float heightOffset = 1f;        // Ajuste de altura relativo al jugador
    public Vector3 positionOffset;         // Offset adicional para ajustar la posición del arma

    [Header("Control de Rotación")]
    public float rotationSpeed = 5f;       // Velocidad de rotación del arma
    public bool limitRotation = false;     // Limitar la rotación del arma
    public float minRotationAngle = -45f;  // Ángulo mínimo de rotación
    public float maxRotationAngle = 45f;   // Ángulo máximo de rotación

    void Update()
    {
        MouseMovementProcess();
    }

    void MouseMovementProcess()
    {
        // Obtener la posición del ratón en el mundo
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Asegúrate de que la Z sea cero para 2D

        // Calcular la dirección hacia el ratón, pero mantener la altura relativa al jugador
        Vector3 targetDir = (new Vector3(mousePos.x, player.position.y + heightOffset, 0) - player.position).normalized;

        // Calcular la nueva posición del arma alrededor del jugador con el offset adicional
        Vector3 desiredPosition = player.position + targetDir * distanceFromPlayer + positionOffset;

        // Mover el arma hacia la posición deseada
        transform.position = Vector3.Lerp(transform.position, desiredPosition, rotationSpeed * Time.deltaTime);

        // Calcular el ángulo de rotación en grados
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

        // Aplicar limitaciones de rotación si están activadas
        if (limitRotation)
        {
            angle = Mathf.Clamp(angle, minRotationAngle, maxRotationAngle);
        }

        // Aplicar la rotación calculada
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
