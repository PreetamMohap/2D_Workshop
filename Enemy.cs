using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public float rotateSpeed = 200f;
    public int health = 1;
    public GameObject coinPrefab;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            DropCoin();
            Destroy(gameObject);
        }
    }

    void DropCoin()
    {
        if (coinPrefab != null)
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
    }
}
