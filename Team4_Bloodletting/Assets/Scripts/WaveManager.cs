using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Analytics;

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
    public GameObject door;
    public GameObject clearHUD;

    private int currentWaveIndex = -1;
    private int enemiesRemaining = 0;
    private int enemiesKilled = 0;
    private int killsNeededForClear = 0;
    private bool waveInProgress = false;
    private bool doorOpened = false;

    private GameHandler gameHandler;

    void Start()
    {
        gameHandler = FindObjectOfType<GameHandler>();
        if (gameHandler == null)
            Debug.LogError("GameHandler not found by WaveManager!");
        else
            Debug.Log("WaveManager found GameHandler successfully.");

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

        StartCoroutine(WaitForTutorialThenStart());

        if (clearHUD != null)
        {
            clearHUD.SetActive(false);
        }
    }

    void Update()
    {
        // Regular wave logic if not yet triggered 80% kill rule
        if (gameHandler.dialogue_complete && !doorOpened)
        {
            if (!waveInProgress && enemiesRemaining == 0)
            {
                if (currentWaveIndex + 1 < waves.Count)
                {
                    float delay = waves[currentWaveIndex + 1].waveDelay;
                    Debug.Log($"Wave {currentWaveIndex + 1} complete. Next wave in {delay} seconds.");
                    StartCoroutine(BeginNextWaveAfterDelay(delay));
                }
                else
                {
                    // All waves done, fallback trigger
                    OpenDoor();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && doorOpened)
        {
            gameHandler.GoToNextLevel();
        }
    }

    IEnumerator BeginNextWaveAfterDelay(float delay)
    {
        waveInProgress = true;
        yield return new WaitForSeconds(delay);

        currentWaveIndex++;

        if (currentWaveIndex >= waves.Count)
        {
            Debug.Log("No more waves.");
            yield break;
        }

        enemiesKilled = 0;
        killsNeededForClear = Mathf.CeilToInt(waves[currentWaveIndex].enemyCount * 0.8f);
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

            yield return new WaitForSeconds(wave.spawnRate);
        }
    }

    public void OnEnemyDied()
    {
        enemiesRemaining--;
        enemiesKilled++;
        Debug.Log($"Enemy died. Killed: {enemiesKilled}, Remaining: {enemiesRemaining}");

        if (!doorOpened && enemiesKilled >= killsNeededForClear)
        {
            Debug.Log("80% of enemies killed. Door opening...");
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        doorOpened = true;

        if (door != null)
        {
            Animator doorAnim = door.GetComponent<Animator>();
            if (doorAnim != null)
                doorAnim.enabled = true;

            BoxCollider2D doorCollider = GetComponent<BoxCollider2D>();
            if (doorCollider != null)
                doorCollider.enabled = true;

            Debug.Log("Door animation and collider activated.");
            clearHUD.SetActive(true);
        }
    }

    IEnumerator WaitForTutorialThenStart()
    {
        Debug.Log("Waiting for tutorial to complete...");
        while (gameHandler != null && !gameHandler.dialogue_complete)
        {
            yield return null;
        }

        Debug.Log("Tutorial complete! Starting waves...");
        StartCoroutine(BeginNextWaveAfterDelay(2f));
    }
}
