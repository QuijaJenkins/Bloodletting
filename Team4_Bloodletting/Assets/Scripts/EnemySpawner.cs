

// using UnityEngine;

// public class EnemySpawner : MonoBehaviour
// {
//     public GameObject enemyPrefab;
//     public Transform[] spawnPoints;
//     public float spawnInterval = 1.5f;
//     public float chaseStartDelay = 15f;

//     private float timer = 0f;
//     private float gameTime = 0f;
//     private int guardIndex = 0;
//     private bool initialGuardsSpawned = false;

//     public static bool chasingAllowed = false;

//     void Update()
//     {
//         timer += Time.deltaTime;
//         gameTime += Time.deltaTime;

//         if (!chasingAllowed && gameTime >= chaseStartDelay)
//         {
//             chasingAllowed = true;
//             Debug.Log("Chasing is now allowed!");
//         }

//         if (timer >= spawnInterval)
//         {
//             timer = 0f;

//             if (!initialGuardsSpawned)
//             {
//                 SpawnInitialGuard();
//             }
//             else
//             {
//                 SpawnRandomEnemy();
//             }
//         }
//     }

//     void SpawnInitialGuard()
//     {
//         if (guardIndex < spawnPoints.Length)
//         {
//             GameObject enemy = Instantiate(enemyPrefab, spawnPoints[guardIndex].position, Quaternion.identity);
//             EnemyChasePlayer behavior = enemy.GetComponent<EnemyChasePlayer>();
//             if (behavior != null)
//             {
//                 behavior.SetAsGuard();
//             }

//             guardIndex++;
//         }

//         if (guardIndex >= spawnPoints.Length)
//         {
//             initialGuardsSpawned = true;
//         }
//     }

//     void SpawnRandomEnemy()
//     {
//         int randomIndex = Random.Range(0, spawnPoints.Length);
//         GameObject enemy = Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
//         // Remaining behavior (chase or idle) is handled in the enemy script
//     }
// }

// using System.Collections;
// using UnityEngine;

// public class EnemySpawner : MonoBehaviour
// {
//     public GameObject enemyPrefab;
//     public int enemiesPerWave = 15;
//     public float waveInterval = 4f;
//     public float spawnInterval = 0.2f;

//     public static bool chasingAllowed = true; // Enable chasing immediately

//     private Camera cam;

//     void Start()
//     {
//         cam = Camera.main;
//         StartCoroutine(WaveLoop());
//     }

//     IEnumerator WaveLoop()
//     {
//         yield return new WaitForSeconds(2f); // Delay before first wave

//         while (true)
//         {
//             for (int i = 0; i < enemiesPerWave; i++)
//             {
//                 SpawnEnemyFromOffScreen();
//                 yield return new WaitForSeconds(spawnInterval);
//             }

//             yield return new WaitForSeconds(waveInterval);
//         }
//     }

//     void SpawnEnemyFromOffScreen()
//     {
//         Vector2 spawnPos = GetRandomOffScreenPosition();
//         GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
//         // Chasing is now handled directly in the enemy script
//     }

//     Vector2 GetRandomOffScreenPosition()
//     {
//         float buffer = 1.5f; // Off-screen distance
//         float screenLeft = cam.ViewportToWorldPoint(new Vector3(0, 0)).x;
//         float screenRight = cam.ViewportToWorldPoint(new Vector3(1, 0)).x;
//         float screenTop = cam.ViewportToWorldPoint(new Vector3(0, 1)).y;
//         float screenBottom = cam.ViewportToWorldPoint(new Vector3(0, 0)).y;

//         int side = Random.Range(0, 4);
//         float x = 0f, y = 0f;

//         switch (side)
//         {
//             case 0: // Top
//                 x = Random.Range(screenLeft, screenRight);
//                 y = screenTop + buffer;
//                 break;
//             case 1: // Bottom
//                 x = Random.Range(screenLeft, screenRight);
//                 y = screenBottom - buffer;
//                 break;
//             case 2: // Left
//                 x = screenLeft - buffer;
//                 y = Random.Range(screenBottom, screenTop);
//                 break;
//             case 3: // Right
//                 x = screenRight + buffer;
//                 y = Random.Range(screenBottom, screenTop);
//                 break;
//         }

//         return new Vector2(x, y);
//     }
// }

// using System.Collections;
// using UnityEngine;

// public class EnemySpawner : MonoBehaviour
// {
//     public GameObject enemyPrefab;
//     public int enemiesPerWave = 20;
//     public float waveInterval = 4f;
//     public float spawnInterval = 0.1f;

//     private Camera cam;

//     void Start()
//     {
//         cam = Camera.main;
//         StartCoroutine(WaveLoop());
//     }

//     IEnumerator WaveLoop()
//     {
//         yield return new WaitForSeconds(1f); // short delay before first wave

//         while (true)
//         {
//             for (int i = 0; i < enemiesPerWave; i++)
//             {
//                 SpawnEnemyFromOffScreen();
//                 yield return new WaitForSeconds(spawnInterval);
//             }

//             yield return new WaitForSeconds(waveInterval);
//         }
//     }

//     void SpawnEnemyFromOffScreen()
//     {
//         Vector2 spawnPos = GetRandomOffScreenPosition();
//         GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
//     }

//     Vector2 GetRandomOffScreenPosition()
//     {
//         float buffer = 2f; // Distance outside camera view
//         float camHeight = 2f * cam.orthographicSize;
//         float camWidth = camHeight * cam.aspect;
//         Vector2 camCenter = cam.transform.position;

//         int side = Random.Range(0, 4); // 0=Top, 1=Bottom, 2=Left, 3=Right
//         float x = 0, y = 0;

//         switch (side)
//         {
//             case 0: // Top
//                 x = Random.Range(camCenter.x - camWidth / 2f, camCenter.x + camWidth / 2f);
//                 y = camCenter.y + camHeight / 2f + buffer;
//                 break;
//             case 1: // Bottom
//                 x = Random.Range(camCenter.x - camWidth / 2f, camCenter.x + camWidth / 2f);
//                 y = camCenter.y - camHeight / 2f - buffer;
//                 break;
//             case 2: // Left
//                 x = camCenter.x - camWidth / 2f - buffer;
//                 y = Random.Range(camCenter.y - camHeight / 2f, camCenter.y + camHeight / 2f);
//                 break;
//             case 3: // Right
//                 x = camCenter.x + camWidth / 2f + buffer;
//                 y = Random.Range(camCenter.y - camHeight / 2f, camCenter.y + camHeight / 2f);
//                 break;
//         }

//         return new Vector2(x, y);
//     }
// }

// using UnityEngine;

// public class EnemySpawner : MonoBehaviour
// {
//     public GameObject enemyPrefab;
//     public float spawnInterval = 1.5f;
//     public float spawnDistanceFromCamera = 10f;
//     public int enemiesPerWave = 3;

//     private float timer = 0f;
//     private Camera cam;

//     void Start()
//     {
//         cam = Camera.main;
//     }

//     void Update()
//     {
//         timer += Time.deltaTime;
//         if (timer >= spawnInterval)
//         {
//             timer = 0f;
//             SpawnWave();
//         }
//     }

//     void SpawnWave()
//     {
//         for (int i = 0; i < enemiesPerWave; i++)
//         {
//             Vector3 spawnPos = GetRandomSpawnPosition();
//             Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
//         }
//     }

//     Vector3 GetRandomSpawnPosition()
//     {
//         Vector3 camPos = cam.transform.position;
//         float camHeight = 2f * cam.orthographicSize;
//         float camWidth = camHeight * cam.aspect;

//         int side = Random.Range(0, 4);
//         float x = 0, y = 0;

//         switch (side)
//         {
//             case 0: // Top
//                 x = Random.Range(camPos.x - camWidth / 2, camPos.x + camWidth / 2);
//                 y = camPos.y + camHeight / 2 + spawnDistanceFromCamera;
//                 break;
//             case 1: // Bottom
//                 x = Random.Range(camPos.x - camWidth / 2, camPos.x + camWidth / 2);
//                 y = camPos.y - camHeight / 2 - spawnDistanceFromCamera;
//                 break;
//             case 2: // Left
//                 x = camPos.x - camWidth / 2 - spawnDistanceFromCamera;
//                 y = Random.Range(camPos.y - camHeight / 2, camPos.y + camHeight / 2);
//                 break;
//             case 3: // Right
//                 x = camPos.x + camWidth / 2 + spawnDistanceFromCamera;
//                 y = Random.Range(camPos.y - camHeight / 2, camPos.y + camHeight / 2);
//                 break;
//         }

//         return new Vector3(x, y, 0f);
//     }
// }


// using UnityEngine;

// public class EnemySpawner : MonoBehaviour
// {
//     public GameObject enemyPrefab;
//     public float spawnInterval = 1.0f;
//     public int enemiesPerWave = 5;
//     public float waveInterval = 3.5f;

//     private float spawnTimer = 0f;
//     private float waveTimer = 0f;
//     private int enemiesSpawnedThisWave = 0;

//     void Update()
//     {
//         waveTimer += Time.deltaTime;

//         if (waveTimer >= waveInterval)
//         {
//             spawnTimer += Time.deltaTime;

//             if (enemiesSpawnedThisWave < enemiesPerWave && spawnTimer >= spawnInterval)
//             {
//                 SpawnEnemyOffScreen();
//                 enemiesSpawnedThisWave++;
//                 spawnTimer = 0f;
//             }

//             if (enemiesSpawnedThisWave >= enemiesPerWave)
//             {
//                 waveTimer = 0f;
//                 enemiesSpawnedThisWave = 0;
//             }
//         }
//     }

//     void SpawnEnemyOffScreen()
//     {
//         Vector2 spawnPos = GetOffScreenPosition();
//         Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
//     }

//     Vector2 GetOffScreenPosition()
//     {
//         Camera cam = Camera.main;
//         float height = 2f * cam.orthographicSize;
//         float width = height * cam.aspect;

//         int side = Random.Range(0, 4); // 0=Top, 1=Right, 2=Bottom, 3=Left

//         Vector2 center = cam.transform.position;
//         float x = 0, y = 0;

//         switch (side)
//         {
//             case 0: // Top
//                 x = Random.Range(center.x - width / 2, center.x + width / 2);
//                 y = center.y + height / 2 + 2f;
//                 break;
//             case 1: // Right
//                 x = center.x + width / 2 + 2f;
//                 y = Random.Range(center.y - height / 2, center.y + height / 2);
//                 break;
//             case 2: // Bottom
//                 x = Random.Range(center.x - width / 2, center.x + width / 2);
//                 y = center.y - height / 2 - 2f;
//                 break;
//             case 3: // Left
//                 x = center.x - width / 2 - 2f;
//                 y = Random.Range(center.y - height / 2, center.y + height / 2);
//                 break;
//         }

//         return new Vector2(x, y);
//     }
// }

// using UnityEngine;

// public class EnemySpawner : MonoBehaviour
// {
//     public GameObject enemyPrefab;

//     void Start()
//     {
//         SpawnOneEnemyOffScreen();
//     }

//     void SpawnOneEnemyOffScreen()
//     {
//         Vector2 spawnPos = GetOffScreenPosition();
//         Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
//     }

//     Vector2 GetOffScreenPosition()
//     {
//         Camera cam = Camera.main;
//         float height = 2f * cam.orthographicSize;
//         float width = height * cam.aspect;
//         Vector2 camCenter = cam.transform.position;

//         // Spawn just outside one random side
//         int side = Random.Range(0, 4); // 0 = top, 1 = right, 2 = bottom, 3 = left
//         float x = 0, y = 0;

//         switch (side)
//         {
//             case 0: y = camCenter.y + height / 2 + 2f; x = Random.Range(camCenter.x - width / 2, camCenter.x + width / 2); break;
//             case 1: x = camCenter.x + width / 2 + 2f; y = Random.Range(camCenter.y - height / 2, camCenter.y + height / 2); break;
//             case 2: y = camCenter.y - height / 2 - 2f; x = Random.Range(camCenter.x - width / 2, camCenter.x + width / 2); break;
//             case 3: x = camCenter.x - width / 2 - 2f; y = Random.Range(camCenter.y - height / 2, camCenter.y + height / 2); break;
//         }

//         return new Vector2(x, y);
//     }
// }

// using UnityEngine;
// using System.Collections;

// public class EnemySpawner : MonoBehaviour
// {
//     public GameObject enemyPrefab;
//     public Transform player;

//     public float spawnRadius = 15f;
//     public float startDelay = 2f;
//     public float waveInterval = 4f;
//     public int initialEnemies = 3;
//     public int extraEnemiesPerWave = 2;
//     public float initialSpawnInterval = 1.2f;
//     public float spawnRateAccel = 0.1f;
//     public float minSpawnInterval = 0.3f;
//     public float baseEnemySpeed = 3f;
//     public float speedGrowth = 0.4f;

//     private int waveNumber = 0;

//     void Start()
//     {
//         if (player == null)
//         {
//             player = GameObject.FindGameObjectWithTag("Player")?.transform;
//         }
//         StartCoroutine(SpawnWaves());
//     }

//     IEnumerator SpawnWaves()
//     {
//         yield return new WaitForSeconds(startDelay);

//         while (true)
//         {
//             waveNumber++;
//             int enemiesThisWave = initialEnemies + extraEnemiesPerWave * (waveNumber - 1);
//             float currentSpawnInterval = Mathf.Max(minSpawnInterval, initialSpawnInterval - spawnRateAccel * (waveNumber - 1));
//             float enemySpeed = baseEnemySpeed + speedGrowth * (waveNumber - 1);

//             for (int i = 0; i < enemiesThisWave; i++)
//             {
//                 SpawnEnemy(enemySpeed);
//                 yield return new WaitForSeconds(currentSpawnInterval);
//             }

//             yield return new WaitForSeconds(waveInterval);
//         }
//     }

//     void SpawnEnemy(float speed)
//     {
//         Vector2 dir = Random.insideUnitCircle.normalized;
//         Vector3 spawnPos = player.position + new Vector3(dir.x, dir.y, 0) * spawnRadius;

//         GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
//         EnemyChasePlayer chaser = enemy.GetComponent<EnemyChasePlayer>();

//         if (chaser != null)
//         {
//             chaser.speed = speed;
//         }
//     }
// }
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
