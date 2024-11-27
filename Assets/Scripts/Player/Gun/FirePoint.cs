using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMovement : MonoBehaviour
{
    [Header("Posicionamiento del Arma")]
    public Transform player;               // Asigna el transform del jugador en el Inspector
    public float distanceFromPlayer = 1f;  // Distancia deseada desde el jugador
    public float heightOffset = 1f;        // Ajuste de altura relativo al jugador
    public Vector3 positionOffset;         // Offset adicional para ajustar la posici�n del arma

    [Header("Control de Rotaci�n")]
    public float rotationSpeed = 5f;       // Velocidad de rotaci�n del arma
    public bool limitRotation = false;     // Limitar la rotaci�n del arma
    public float minRotationAngle = -45f;  // �ngulo m�nimo de rotaci�n
    public float maxRotationAngle = 45f;   // �ngulo m�ximo de rotaci�n

    void Update()
    {
        MouseMovementProcess();
    }

    void MouseMovementProcess()
    {
        // Obtener la posici�n del rat�n en el mundo
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Aseg�rate de que la Z sea cero para 2D

        // Calcular la direcci�n hacia el rat�n, pero mantener la altura relativa al jugador
        Vector3 targetDir = (new Vector3(mousePos.x, player.position.y + heightOffset, 0) - player.position).normalized;

        // Calcular la nueva posici�n del arma alrededor del jugador con el offset adicional
        Vector3 desiredPosition = player.position + targetDir * distanceFromPlayer + positionOffset;

        // Mover el arma hacia la posici�n deseada
        transform.position = Vector3.Lerp(transform.position, desiredPosition, rotationSpeed * Time.deltaTime);

        // Calcular el �ngulo de rotaci�n en grados
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

        // Aplicar limitaciones de rotaci�n si est�n activadas
        if (limitRotation)
        {
            angle = Mathf.Clamp(angle, minRotationAngle, maxRotationAngle);
        }

        // Aplicar la rotaci�n calculada
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
