// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;

// public class WaveManager : MonoBehaviour
// {
//     [System.Serializable]
//     public class Wave
//     {
//         public int enemyCount;
//         public float spawnRate = 1f;
//         public float spawnRateJitter = 0.3f;
//         public float minSpeedMultiplier = 1f;
//         public float maxSpeedMultiplier = 1.5f;
//         public float waveDelay = 2f; // pause between waves
//     }

//     public List<Wave> waves;
//     public GameObject enemyPrefab;
//     public float spawnDistanceFromPlayer = 20f;
//     public int maxEnemiesOnScreen = 10;

//     private int currentWaveIndex = -1;
//     private bool isSpawning = false;
//     private bool waveInProgress = false;
//     private Transform player;

//     void Start()
//     {
//         Debug.Log("WaveManager is alive");

//         player = GameObject.FindGameObjectWithTag("Player")?.transform;
//         if (player == null) Debug.LogError("Player not found!");
//         if (enemyPrefab == null) Debug.LogError("Enemy prefab not assigned!");

//         StartCoroutine(StartNextWave());
//     }

//     void Update()
//     {
//         if (!waveInProgress && !isSpawning && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
//         {
//             StartCoroutine(StartNextWave());
//         }
//     }

//     IEnumerator StartNextWave()
//     {
//         currentWaveIndex++;

//         if (currentWaveIndex >= waves.Count)
//         {
//             Debug.Log("All waves complete!");
//             yield break;
//         }

//         Wave wave = waves[currentWaveIndex];

//         // Short pause between waves
//         Debug.Log($"Wave {currentWaveIndex + 1} starting in {wave.waveDelay} seconds...");
//         waveInProgress = true;
//         yield return new WaitForSeconds(wave.waveDelay);

//         Debug.Log($"WAVE {currentWaveIndex + 1} STARTING: {wave.enemyCount} enemies");

//         isSpawning = true;
//         int spawned = 0;

//         while (spawned < wave.enemyCount)
//         {
//             if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemiesOnScreen)
//             {
//                 Vector2 spawnPos = GetRandomOffScreenSpawnPosition();
//                 GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

//                 float speed = Random.Range(wave.minSpeedMultiplier, wave.maxSpeedMultiplier);
//                 var chase = enemy.GetComponent<EnemyChasePlayer>();
//                 if (chase != null) chase.SetSpeedMultiplier(speed);

//                 Debug.Log($"Spawned enemy #{spawned + 1} at {spawnPos} with speed {speed:F2}");

//                 spawned++;

//                 float delay = wave.spawnRate + Random.Range(-wave.spawnRateJitter, wave.spawnRateJitter);
//                 yield return new WaitForSeconds(Mathf.Clamp(delay, 0.2f, 5f));
//             }
//             else
//             {
//                 yield return null;
//             }
//         }

//         isSpawning = false;
//         waveInProgress = false;
//     }

//     Vector2 GetRandomOffScreenSpawnPosition()
//     {
//         if (player == null) return Vector2.zero;

//         Vector2 randomDirection = Random.insideUnitCircle.normalized;
//         return (Vector2)player.position + randomDirection * spawnDistanceFromPlayer;
//     }
// }

// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;

// public class WaveManager : MonoBehaviour
// {
//     [System.Serializable]
//     public class Wave
//     {
//         public int enemyCount = 5;
//         public float spawnRate = 1f;
//         public float minSpeedMultiplier = 1f;
//         public float maxSpeedMultiplier = 1.5f;
//         public float waveDelay = 3f; // Time between waves
//     }

//     public List<Wave> waves;
//     public GameObject enemyPrefab;
//     public float spawnDistanceFromPlayer = 20f;
//     public int maxEnemiesOnScreen = 10;

//     private int currentWaveIndex = -1;
//     private bool isSpawning = false;
//     private bool waitingForNextWave = false;
//     private Transform player;

//     void Start()
//     {
//         player = GameObject.FindGameObjectWithTag("Player")?.transform;
//         if (player == null) Debug.LogError("Player not found!");
//         if (enemyPrefab == null) Debug.LogError("Enemy prefab not assigned!");

//         StartCoroutine(WaitAndStartNextWave(2f)); // initial delay before first wave
//     }

//     void Update()
//     {
//         if (!isSpawning && !waitingForNextWave && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
//         {
//             // All enemies killed, start next wave after delay
//             if (currentWaveIndex + 1 < waves.Count)
//             {
//                 Wave nextWave = waves[currentWaveIndex + 1];
//                 Debug.Log($"All enemies cleared. Wave {currentWaveIndex + 2} starting in {nextWave.waveDelay} seconds...");
//                 StartCoroutine(WaitAndStartNextWave(nextWave.waveDelay));
//             }
//             else
//             {
//                 Debug.Log("All waves complete!");
//             }
//         }
//     }

//     IEnumerator WaitAndStartNextWave(float delay)
//     {
//         waitingForNextWave = true;
//         yield return new WaitForSeconds(delay);
//         waitingForNextWave = false;
//         StartCoroutine(StartWave());
//     }

//     IEnumerator StartWave()
//     {
//         currentWaveIndex++;

//         if (currentWaveIndex >= waves.Count)
//         {
//             Debug.Log("All waves complete!");
//             yield break;
//         }

//         Wave wave = waves[currentWaveIndex];
//         isSpawning = true;

//         Debug.Log($"WAVE {currentWaveIndex + 1} STARTING: {wave.enemyCount} enemies");

//         for (int i = 0; i < wave.enemyCount; i++)
//         {
//             while (GameObject.FindGameObjectsWithTag("Enemy").Length >= maxEnemiesOnScreen)
//                 yield return null;

//             Vector2 spawnPos = GetRandomOffScreenSpawnPosition();
//             GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

//             float speed = Random.Range(wave.minSpeedMultiplier, wave.maxSpeedMultiplier);
//             var chase = enemy.GetComponent<EnemyChasePlayer>();
//             if (chase != null) chase.SetSpeedMultiplier(speed);

//             Debug.Log($"Spawned enemy {i + 1}/{wave.enemyCount} at {spawnPos} (speed {speed:F2})");

//             yield return new WaitForSeconds(wave.waveDelay);


//             // yield return new WaitForSeconds(wave.spawnRate); // delay between individual spawns
//         }

//         isSpawning = false;
//     }

//     Vector2 GetRandomOffScreenSpawnPosition()
//     {
//         if (player == null) return Vector2.zero;

//         Vector2 direction = Random.insideUnitCircle.normalized;
//         float distance = spawnDistanceFromPlayer + Random.Range(2f, 6f); // a little variation
//         return (Vector2)player.position + direction * distance;
//     }
// }

// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;

// public class WaveManager : MonoBehaviour
// {
//     [System.Serializable]
//     public class Wave
//     {
//         public int enemyCount = 5;
//         public float spawnRate = 1f;
//         public float minSpeedMultiplier = 1f;
//         public float maxSpeedMultiplier = 1.5f;
//         public float waveDelay = 7f; // Delay before next wave starts
//     }

//     public List<Wave> waves;
//     public GameObject enemyPrefab;
//     public float spawnDistanceFromPlayer = 20f;
//     public int maxEnemiesOnScreen = 10;

//     private int currentWaveIndex = -1;
//     private int enemiesSpawnedThisWave = 0;
//     private bool isSpawning = false;
//     private bool waitingForNextWave = false;
//     private Transform player;

//     void Start()
//     {
//         player = GameObject.FindGameObjectWithTag("Player")?.transform;

//         if (player == null)
//             Debug.LogError("Player not found!");
//         if (enemyPrefab == null)
//             Debug.LogError("Enemy prefab not assigned!");

//         StartCoroutine(PrepareNextWave(2f)); // Initial delay
//     }

// void Update()
// {
//     if (!isSpawning && !waitingForNextWave)
//     {
//         Debug.Log($"Update running — WaveIndex: {currentWaveIndex}");

//         if (currentWaveIndex >= 0)
//         {
//             int enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
//             bool allEnemiesDead = enemiesAlive == 0;
//             bool waveFullySpawned = enemiesSpawnedThisWave == waves[currentWaveIndex].enemyCount;

//             Debug.Log($"Enemies Alive: {enemiesAlive}, Spawned: {enemiesSpawnedThisWave}/{waves[currentWaveIndex].enemyCount}");
//             // Debug.Log($"Enemy Count: {waves.enemyCount}");

//             if (allEnemiesDead && waveFullySpawned)
//             {
//                 if (currentWaveIndex + 1 < waves.Count)
//                 {
//                     Wave nextWave = waves[currentWaveIndex + 1];
//                     Debug.Log($"Wave {currentWaveIndex + 1} complete. Wave {currentWaveIndex + 2} starting in {nextWave.waveDelay} seconds...");
//                     waitingForNextWave = true;
//                     StartCoroutine(PrepareNextWave(nextWave.waveDelay));
//                 }
//                 else
//                 {
//                     Debug.Log("All waves complete!");
//                 }
//             }
//         }
//     }
// }



//     // void Update()
//     // {
//     //     if (!isSpawning && !waitingForNextWave)
//     //     {
//     //         bool allEnemiesDead = GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
//     //         bool waveIsComplete = enemiesSpawnedThisWave == waves[currentWaveIndex].enemyCount;

//     //         if (allEnemiesDead && waveIsComplete)
//     //         {
//     //             if (currentWaveIndex + 1 < waves.Count)
//     //             {
//     //                 Wave nextWave = waves[currentWaveIndex + 1];
//     //                 Debug.Log($"Wave {currentWaveIndex + 1} complete. Wave {currentWaveIndex + 2} starting in {nextWave.waveDelay} seconds...");
//     //                 waitingForNextWave = true;
//     //                 StartCoroutine(PrepareNextWave(nextWave.waveDelay));
//     //             }
//     //             else
//     //             {
//     //                 Debug.Log("All waves complete!");
//     //             }
//     //         }
//     //     }
//     // }

//     IEnumerator PrepareNextWave(float delay)
//     {
//         Debug.Log($"Waiting {delay} seconds before next wave...");
//         yield return new WaitForSeconds(delay);
//         waitingForNextWave = false;
//         StartCoroutine(StartWave());
//     }

//     IEnumerator StartWave()
//     {
//         currentWaveIndex++;

//         if (currentWaveIndex >= waves.Count)
//         {
//             Debug.Log("No more waves.");
//             yield break;
//         }

//         Wave wave = waves[currentWaveIndex];
//         enemiesSpawnedThisWave = 0; // Reset for new wave
//         isSpawning = true;

//         Debug.Log($"WAVE {currentWaveIndex + 1} STARTING: {wave.enemyCount} enemies");

//         for (int i = 0; i < wave.enemyCount; i++)
//         {
//             while (GameObject.FindGameObjectsWithTag("Enemy").Length >= maxEnemiesOnScreen)
//                 yield return null;

//             Vector2 spawnPos = GetRandomOffScreenSpawnPosition();
//             GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

//             float speed = Random.Range(wave.minSpeedMultiplier, wave.maxSpeedMultiplier);
//             var chase = enemy.GetComponent<EnemyChasePlayer>();
//             if (chase != null)
//                 chase.SetSpeedMultiplier(speed);

//             enemiesSpawnedThisWave++;
//             Debug.Log($"Spawned enemy {i + 1}/{wave.enemyCount} at {spawnPos} (speed {speed:F2})");

//             yield return new WaitForSeconds(wave.spawnRate);
//         }

//         isSpawning = false;
//     }

//     Vector2 GetRandomOffScreenSpawnPosition()
//     {
//         if (player == null) return Vector2.zero;

//         Vector2 direction = Random.insideUnitCircle.normalized;
//         float distance = spawnDistanceFromPlayer + Random.Range(2f, 5f);
//         return (Vector2)player.position + direction * distance;
//     }
// }

// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;

// public class WaveManager : MonoBehaviour
// {
//     [System.Serializable]
//     public class Wave
//     {
//         public int enemyCount = 1;
//         public float spawnRate = 1f;
//         public float minSpeedMultiplier = 1f;
//         public float maxSpeedMultiplier = 1.5f;
//         public float waveDelay = 5f;
//     }

//     public List<Wave> waves;
//     public GameObject enemyPrefab;
//     public float spawnDistanceFromPlayer = 20f;
//     public int maxEnemiesOnScreen = 3;

//     private int currentWaveIndex = -1;
//     private int enemiesSpawnedThisWave = 0;
//     private int enemiesAlive = 0;
//     private bool isSpawning = false;
//     private Transform player;

//     void Start()
//     {
//         player = GameObject.FindGameObjectWithTag("Player")?.transform;
//         if (player == null) Debug.LogError("Player not found!");
//         if (enemyPrefab == null) Debug.LogError("Enemy prefab not assigned!");

//         StartCoroutine(BeginWaveAfterDelay(2f));
//     }

//     void Update()
//     {
//         // Wait until all enemies are dead before proceeding
//         if (!isSpawning && enemiesAlive == 0 && currentWaveIndex >= 0)
//         {
//             if (currentWaveIndex + 1 < waves.Count)
//             {
//                 float delay = waves[currentWaveIndex + 1].waveDelay;
//                 Debug.Log($"✅ Wave {currentWaveIndex + 1} complete. Next wave in {delay} seconds.");
//                 StartCoroutine(BeginWaveAfterDelay(delay));
//             }
//             else
//             {
//                 Debug.Log("🏁 All waves complete.");
//             }
//         }
//     }

//     IEnumerator BeginWaveAfterDelay(float delay)
//     {
//         isSpawning = true;
//         yield return new WaitForSeconds(delay);
//         StartCoroutine(StartWave());
//     }

//     IEnumerator StartWave()
//     {
//         currentWaveIndex++;
//         if (currentWaveIndex >= waves.Count)
//         {
//             Debug.Log("🚫 No more waves.");
//             yield break;
//         }

//         Wave wave = waves[currentWaveIndex];
//         enemiesSpawnedThisWave = 0;
//         enemiesAlive = 0;

//         Debug.Log($"🚨 WAVE {currentWaveIndex + 1} STARTING | Spawning {wave.enemyCount} enemies");

//         for (int i = 0; i < wave.enemyCount; i++)
//         {
//             if (GameObject.FindGameObjectsWithTag("Enemy").Length >= maxEnemiesOnScreen)
//             {
//                 Debug.LogWarning("⚠️ Max enemies on screen reached, delaying...");
//                 yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemiesOnScreen);
//             }

//             Vector2 spawnPos = GetSpawnPosition();
//             GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

//             float speed = Random.Range(wave.minSpeedMultiplier, wave.maxSpeedMultiplier);
//             EnemyChasePlayer chase = enemy.GetComponent<EnemyChasePlayer>();
//             if (chase != null) chase.SetSpeedMultiplier(speed);

//             enemiesSpawnedThisWave++;
//             enemiesAlive++;
//             enemy.name = $"Wave{currentWaveIndex + 1}_Enemy{i + 1}";
//             Debug.Log($"👾 Spawned Enemy {i + 1}/{wave.enemyCount} at {spawnPos}");

//             EnemyDeathNotifier deathNotifier = enemy.AddComponent<EnemyDeathNotifier>();
//             deathNotifier.manager = this;

//             yield return new WaitForSeconds(wave.spawnRate);
//         }

//         isSpawning = false;
//     }

//     Vector2 GetSpawnPosition()
//     {
//         if (player == null) return Vector2.zero;
//         Vector2 dir = Random.insideUnitCircle.normalized;
//         return (Vector2)player.position + dir * spawnDistanceFromPlayer;
//     }

//     public void OnEnemyDied()
//     {
//         enemiesAlive--;
//         Debug.Log($"💀 Enemy died. Remaining: {enemiesAlive}");
//     }
// }
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
    public float spawnDistanceFromPlayer = 20f;

    private Transform player;
    private int currentWaveIndex = -1;
    private int enemiesRemaining = 0;
    private bool waveInProgress = false;

    void Start()
    {
        Debug.Log("WaveManager started");

        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Player not found! Make sure Player is tagged 'Player'");
            return;
        }

        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab not assigned!");
            return;
        }

        StartCoroutine(StartNextWaveAfterDelay(2f)); // Initial delay
    }

    void Update()
    {
        // Check if wave is complete and there's another wave
        if (!waveInProgress && enemiesRemaining == 0 && currentWaveIndex + 1 < waves.Count)
        {
            float delay = waves[currentWaveIndex + 1].waveDelay;
            Debug.Log($"Wave {currentWaveIndex + 1} complete. Next wave in {delay} seconds.");
            StartCoroutine(StartNextWaveAfterDelay(delay));
        }
    }

    IEnumerator StartNextWaveAfterDelay(float delay)
    {
        waveInProgress = true;
        yield return new WaitForSeconds(delay);

        currentWaveIndex++;
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
        Debug.Log($"Starting Wave {currentWaveIndex + 1} with {wave.enemyCount} enemies.");

        for (int i = 0; i < wave.enemyCount; i++)
        {
            Vector2 spawnPos = GetSpawnPosition();
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            EnemyChasePlayer chase = enemy.GetComponent<EnemyChasePlayer>();
            if (chase != null)
            {
                float speed = Random.Range(wave.minSpeedMultiplier, wave.maxSpeedMultiplier);
                chase.SetSpeedMultiplier(speed);
            }

            EnemyDeathNotifier notifier = enemy.GetComponent<EnemyDeathNotifier>();
            if (notifier == null)
                notifier = enemy.AddComponent<EnemyDeathNotifier>();
            notifier.manager = this;

            Debug.Log($"Spawned enemy {i + 1} at {spawnPos}");
            yield return new WaitForSeconds(wave.spawnRate);
        }
    }

    Vector2 GetSpawnPosition()
    {
        if (player == null) return Vector2.zero;

        Vector2 dir = Random.insideUnitCircle.normalized;
        return (Vector2)player.position + dir * spawnDistanceFromPlayer;
    }

    public void OnEnemyDied()
    {
        enemiesRemaining--;
        Debug.Log($"Enemy killed. Remaining in wave: {enemiesRemaining}");
    }
}
