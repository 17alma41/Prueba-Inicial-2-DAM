using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform firePoint;     // Punto desde donde se disparar� la bala
    public float bulletSpeed = 20f; // Velocidad de la bala

    void Update()
    {
        // Si se hace clic con el bot�n izquierdo del rat�n
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Crear la bala en el firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position + new Vector3(0, 1, 1), firePoint.rotation); // Elevar el spawn

        // Obtener la direcci�n hacia el rat�n
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Asegurarse de que la bala se mantenga en el plano 2D
        Vector2 direction = (mousePosition - firePoint.position).normalized;

        // Aplicar gravedad a la bala
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;
        rb.gravityScale = 1; // A�adir gravedad realista

        // Destruir la bala despu�s de 2 segundos
        Destroy(bullet, 2f);
    }
}
