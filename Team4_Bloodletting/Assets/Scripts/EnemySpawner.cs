


// using UnityEngine;

// public class EnemySpawner : MonoBehaviour
// {
//     public GameObject enemyPrefab;
//     public Transform[] spawnPoints;
//     public float spawnInterval = 1.5f;
//     public float chaseStartDelay = 15f; // Delay before chasing can begin

//     private float timer = 0f;
//     private float gameTime = 0f;
//     private int orderedIndex = 0;
//     private bool initialWaveDone = false;

//     public static bool chasingAllowed = false; // Global flag

//     void Update()
//     {
//         timer += Time.deltaTime;
//         gameTime += Time.deltaTime;

//         // Activate global chase mode after the delay
//         if (!chasingAllowed && gameTime >= chaseStartDelay)
//         {
//             chasingAllowed = true;
//             Debug.Log("Chasing is now allowed!");
//         }

//         if (timer >= spawnInterval)
//         {
//             timer = 0f;

//             if (!initialWaveDone)
//             {
//                 SpawnEnemyInOrder();
//             }
//             else
//             {
//                 SpawnEnemyRandom();
//             }
//         }
//     }

//     void SpawnEnemyInOrder()
//     {
//         if (orderedIndex < spawnPoints.Length)
//         {
//             Instantiate(enemyPrefab, spawnPoints[orderedIndex].position, Quaternion.identity);
//             orderedIndex++;
//         }

//         if (orderedIndex >= spawnPoints.Length)
//         {
//             initialWaveDone = true;
//         }
//     }

//     void SpawnEnemyRandom()
//     {
//         int randomIndex = Random.Range(0, spawnPoints.Length);
//         Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
//     }
// }

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 1.5f;
    public float chaseStartDelay = 15f;

    private float timer = 0f;
    private float gameTime = 0f;
    private int guardIndex = 0;
    private bool initialGuardsSpawned = false;

    public static bool chasingAllowed = false;

    void Update()
    {
        timer += Time.deltaTime;
        gameTime += Time.deltaTime;

        if (!chasingAllowed && gameTime >= chaseStartDelay)
        {
            chasingAllowed = true;
            Debug.Log("Chasing is now allowed!");
        }

        if (timer >= spawnInterval)
        {
            timer = 0f;

            if (!initialGuardsSpawned)
            {
                SpawnInitialGuard();
            }
            else
            {
                SpawnRandomEnemy();
            }
        }
    }

    void SpawnInitialGuard()
    {
        if (guardIndex < spawnPoints.Length)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoints[guardIndex].position, Quaternion.identity);
            EnemyChasePlayer behavior = enemy.GetComponent<EnemyChasePlayer>();
            if (behavior != null)
            {
                behavior.SetAsGuard();
            }

            guardIndex++;
        }

        if (guardIndex >= spawnPoints.Length)
        {
            initialGuardsSpawned = true;
        }
    }

    void SpawnRandomEnemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        GameObject enemy = Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
        // Remaining behavior (chase or idle) is handled in the enemy script
    }
}
