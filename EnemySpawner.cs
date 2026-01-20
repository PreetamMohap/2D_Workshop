using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 1.5f;
    public float xRange = 4f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnRate);
    }

    void SpawnEnemy()
    {
        Vector3 spawnPos = new Vector3(
            Random.Range(-xRange, xRange),
            transform.position.y,
            0f
        );

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
