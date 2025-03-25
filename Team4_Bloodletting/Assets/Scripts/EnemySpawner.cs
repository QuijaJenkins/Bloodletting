
// using UnityEngine;

// public class EnemySpawner : MonoBehaviour
// {
//     public GameObject enemyPrefab;
//     public float spawnRate = 2f;

//     private float timer;

//     void Update()u
//     {
//         timer += Time.deltaTime;
//         if (timer >= spawnRate)
//         {
//             SpawnEnemy();
//             timer = 0f;
//         }
//     }

//     void SpawnEnemy()
//     {
//         Vector2 spawnPos = GetRightEdgeSpawnPosition();
//         Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
//     }

//     Vector2 GetRightEdgeSpawnPosition()
//     {
//         Camera cam = Camera.main;
//         float camHeight = 2f * cam.orthographicSize;
//         float camWidth = camHeight * cam.aspect;
//         Vector2 camCenter = cam.transform.position;

//         float x = camCenter.x + camWidth / 2f + 1f; // slightly offscreen to the right
//         float y = Random.Range(camCenter.y - camHeight / 2f + 1f, camCenter.y + camHeight / 2f - 1f); // anywhere vertically in view

//         return new Vector2(x, y);
//     }
// }

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float baseSpawnRate = 2f;      // Starting rate
    public float minSpawnRate = 0.3f;     // Hard cap (optional)
    public float difficultyRampTime = 60f; // Time (seconds) until max difficulty

    private float spawnTimer;
    private float gameTimer;

    void Update()
    {
        spawnTimer += Time.deltaTime;
        gameTimer += Time.deltaTime;

        float currentSpawnRate = Mathf.Lerp(baseSpawnRate, minSpawnRate, gameTimer / difficultyRampTime);

        if (spawnTimer >= currentSpawnRate)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPos = GetRightEdgeSpawnPosition();
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    Vector2 GetRightEdgeSpawnPosition()
    {
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;
        Vector2 camCenter = cam.transform.position;

        float x = camCenter.x + camWidth / 2f + 1f;
        float y = Random.Range(camCenter.y - camHeight / 2f + 1f, camCenter.y + camHeight / 2f - 1f);

        return new Vector2(x, y);
    }
}
