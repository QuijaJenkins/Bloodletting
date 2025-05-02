
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public int enemyCount = 1;
        public float spawnRate = 1f;
        public float minSpeedMultiplier = 1f;
        public float maxSpeedMultiplier = 1.5f;
        public float waveDelay = 5f;
    }

    public List<Wave> waves;
    public GameObject enemyPrefab;
    public List<Transform> spawnPoints;

    private int currentWaveIndex = -1;
    private int enemiesRemaining = 0;
    private bool waveInProgress = false;

    //added vars
    //public bool wavesComplete = false;

    void Start()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab not assigned!");
            return;
        }

        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Debug.LogError("Spawn points not assigned!");
            return;
        }

        StartCoroutine(BeginNextWaveAfterDelay(2f)); // Initial delay before wave 1
    }

    void Update()
    {
        // When all enemies are dead and we're ready for a new wave
        if (!waveInProgress && enemiesRemaining == 0 && currentWaveIndex + 1 < waves.Count)
        {
            float delay = waves[currentWaveIndex + 1].waveDelay;
            Debug.Log($"Wave {currentWaveIndex + 1} complete. Next wave in {delay} seconds.");
            StartCoroutine(BeginNextWaveAfterDelay(delay));
        }

        // // end of waves for level transition
        // if (waves.Count == 0) {
        //     wavesComplete = true;
        // }
    }

    IEnumerator BeginNextWaveAfterDelay(float delay)
    {
        waveInProgress = true;
        yield return new WaitForSeconds(delay);

        currentWaveIndex++;  // ✅ Safely increment index before accessing the list

        if (currentWaveIndex >= waves.Count)
        {
            Debug.Log("All waves complete!");
            yield break;
        }

        yield return StartCoroutine(SpawnWave(waves[currentWaveIndex]));
        waveInProgress = false;
    }

    IEnumerator SpawnWave(Wave wave)
    {
        enemiesRemaining = wave.enemyCount;
        Debug.Log($"Spawning Wave {currentWaveIndex + 1} with {wave.enemyCount} enemies.");

        for (int i = 0; i < wave.enemyCount; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            // Set random speed for each enemy
            EnemyChasePlayer chase = enemy.GetComponent<EnemyChasePlayer>();
            if (chase != null)
            {
                float speed = Random.Range(wave.minSpeedMultiplier, wave.maxSpeedMultiplier);
                chase.SetSpeedMultiplier(speed);
            }

            // Track when the enemy dies
            EnemyDeathNotifier notifier = enemy.GetComponent<EnemyDeathNotifier>();
            if (notifier == null)
                notifier = enemy.AddComponent<EnemyDeathNotifier>();
            notifier.manager = this;

            Debug.Log($"Spawned enemy {i + 1} at {spawnPoint.name}");
            yield return new WaitForSeconds(wave.spawnRate);
        }
    }

    public void OnEnemyDied()
    {
        enemiesRemaining--;
        Debug.Log($"Enemy died. Remaining in wave: {enemiesRemaining}");
    }
}

