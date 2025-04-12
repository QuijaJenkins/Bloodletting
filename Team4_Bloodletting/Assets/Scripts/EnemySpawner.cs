
using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;

    public float spawnRadius = 18f;
    public float startDelay = 5f;
    public float waveInterval = 10f;
    public int initialEnemies = 1;
    public int enemiesPerWave = 1;
    public float initialSpawnInterval = 5.5f;
    public float spawnIntervalDecay = 0.1f;
    public float minSpawnInterval = 3.5f;
    public float baseEnemySpeed = 1.8f;
    public float speedIncreasePerWave = 0.1f;

    private int waveCount = 0;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        StartCoroutine(SpawnEnemyWaves());
    }

    IEnumerator SpawnEnemyWaves()
    {
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            waveCount++;
            int enemyCount = initialEnemies + enemiesPerWave * (waveCount - 1);
            float currentInterval = Mathf.Max(minSpawnInterval, initialSpawnInterval - spawnIntervalDecay * (waveCount - 1));
            float currentSpeed = baseEnemySpeed + speedIncreasePerWave * (waveCount - 1);

            Debug.Log($"Wave {waveCount} → Enemies: {enemyCount}, Speed: {currentSpeed}, Interval: {currentInterval}");

            for (int i = 0; i < enemyCount; i++)
            {
                SpawnOneEnemy(currentSpeed);
                yield return new WaitForSeconds(currentInterval);
            }

            yield return new WaitForSeconds(waveInterval);
        }
    }

    void SpawnOneEnemy(float speed)
    {
        Vector2 dir = Random.insideUnitCircle.normalized;
        Vector3 spawnPos = player.position + (Vector3)(dir * spawnRadius);

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        EnemyChasePlayer chaser = enemy.GetComponent<EnemyChasePlayer>();

        if (chaser != null)
        {
            chaser.speed = speed;
        }
    }
}
