using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public float levitationSpeed = 1f;    // Velocidad de la levitaci�n
    public float levitationHeight = 0.5f; // Altura m�xima de la levitaci�n
    public float rotationSpeed = 50f;     // Velocidad de rotaci�n
    public string nextSceneName;          // Nombre de la siguiente escena

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;  // Guardamos la posici�n inicial
    }

    private void Update()
    {
        // Movimiento de levitaci�n
        float newY = startPos.y + Mathf.Sin(Time.time * levitationSpeed) * levitationHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Rotaci�n sobre su propio eje
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el jugador ha tocado el portal
        if (other.CompareTag("Player"))
        {
            // Cambiar a la siguiente escena
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
