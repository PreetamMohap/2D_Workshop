using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceshipController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Screen Bounds")]
    public float padding = 0.5f;

    private Camera mainCam;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.2f;

    private float nextFireTime;

    public int maxHealth = 3;
    private int currentHealth;


    void Start()
    {
        mainCam = Camera.main;
        currentHealth = maxHealth;
    }

    void Update()
    {
        Move();
        ClampToScreen();
        Shoot();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, moveY, 0f);
        transform.position += movement * moveSpeed * Time.deltaTime;
    }

    void ClampToScreen()
    {
        Vector3 pos = transform.position;

        Vector3 minBounds = mainCam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxBounds = mainCam.ViewportToWorldPoint(new Vector3(1, 1, 0));

        pos.x = Mathf.Clamp(pos.x, minBounds.x + padding, maxBounds.x - padding);
        pos.y = Mathf.Clamp(pos.y, minBounds.y + padding, maxBounds.y - padding);

        transform.position = pos;
    }
    void Shoot()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage();
            Destroy(other.gameObject);
        }
    }
    void TakeDamage()
    {
        currentHealth--;

        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            RestartGame();
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

