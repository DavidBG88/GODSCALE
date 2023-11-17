using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;  // Prefab de la bala
    private float moveSpeed = 5f;     // Velocidad de movimiento
    private float bulletSpeed = 15f;  // Velocidad de las balas
    private float bulletDestroyTime = 3f;  // Tiempo antes de que las balas se destruyan
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject meleePrefab;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Movimiento del jugador con las teclas W, A, S, D
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(horizontalInput * moveSpeed, verticalInput * moveSpeed, 0);

        // Disparo de balas al hacer clic izquierdo del ratón
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        // Ataque con Melee
        if (Input.GetButtonDown("Jump"))
        {
            Attack();
        }
    }

    void Shoot()
    {
        // Crear una instancia de la bala en la posición del jugador
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

        // Obtener el componente Rigidbody2D de la bala y aplicar velocidad
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        bulletRigidbody.velocity = bullet.transform.right * bulletSpeed;

        // Destruir la bala después de bulletDestroyTime segundos
        Destroy(bullet, bulletDestroyTime);
    }

    void Attack()
    {
        // Crear instancia Melee
        GameObject melee = Instantiate(meleePrefab, transform.position, transform.rotation);

        // Obtener el componente Rigidbody2D del melee y aplicar velocidad
        Rigidbody2D meleeRigidbody = melee.GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(melee.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        meleeRigidbody.velocity = melee.transform.right * bulletSpeed;

        // Destruir el melee después de 0.2 segundos
        Destroy(melee, 0.1f);
    }
}
