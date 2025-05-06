

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;

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
    private bool levelEnd = false;

    private GameHandler gameHandler;
    public GameObject door;
    public GameObject clearHUD;

    //SFX
    public AudioSource enemydeath_sfx;
    public AudioSource enemy_ambient_sfx;
    public TextMeshProUGUI enemyCounterText;

    private List<GameObject> activeEnemies = new List<GameObject>();
    private int enemiesKilled = 0;
    private int killsNeededForWaveClear = 0;
    private bool waveClearedEarly = false;

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
        if (gameHandler.dialogue_complete)
        {
            if (!waveInProgress && waveClearedEarly)
            {
                if (currentWaveIndex + 1 < waves.Count)
                {
                    float delay = waves[currentWaveIndex + 1].waveDelay;
                    Debug.Log($"Wave {currentWaveIndex + 1} complete. Next wave in {delay} seconds.");
                    StartCoroutine(BeginNextWaveAfterDelay(delay));
                }
                else
                {
                    if (gameHandler != null)
                    {
                        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                        if (currentSceneIndex == 4)
                        {
                            GetComponent<HoldenOtherTemp>().enabled = true;
                            levelEnd = true;
                        }
                        else
                        {
                            door.GetComponent<Animator>().enabled = true;
                            GetComponent<BoxCollider2D>().enabled = true;
                            clearHUD.SetActive(true);
                            levelEnd = true;
                        }
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
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
        activeEnemies.Clear();
        waveClearedEarly = false;
        killsNeededForWaveClear = Mathf.CeilToInt(waves[currentWaveIndex].enemyCount * 0.8f);

        UpdateEnemyCounterUI();
        yield return StartCoroutine(SpawnWave(waves[currentWaveIndex]));
        waveInProgress = false;
    }

    IEnumerator SpawnWave(Wave wave)
    {
        if (enemy_ambient_sfx != null && !enemy_ambient_sfx.isPlaying)
        {
            enemy_ambient_sfx.loop = true;
            enemy_ambient_sfx.Play();
        }
        enemiesRemaining = wave.enemyCount;
        Debug.Log($"Spawning Wave {currentWaveIndex + 1} with {wave.enemyCount} enemies.");

        for (int i = 0; i < wave.enemyCount; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            activeEnemies.Add(enemy);


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

        UpdateEnemyCounterUI();
    }

    public void OnEnemyDied()
    {
        if (!waveClearedEarly) {
            enemydeath_sfx.PlayOneShot(enemydeath_sfx.clip);  // sfx
            enemiesRemaining--;
            enemiesKilled++;
            UpdateEnemyCounterUI();
        }

        Debug.Log($"Enemy died. Remaining in wave: {enemiesRemaining}");

        if (enemiesKilled >= killsNeededForWaveClear)
        {
            waveClearedEarly = true;
            Debug.Log("80% or more of wave killed. Clearing wave.");
            StartCoroutine(ClearWaveAfterFullKill());
        }
    }

    IEnumerator FadeOutRemainingEnemies()
    {
        foreach (GameObject enemy in activeEnemies)
        {

            if (enemy != null && enemy.activeSelf)
            {
                enemy.GetComponent<Collider2D>().enabled = false;
                EnemyChasePlayer chase = enemy.GetComponent<EnemyChasePlayer>();
                if (chase != null) chase.enabled = false;

                SpriteRenderer sr = enemy.GetComponentInChildren<SpriteRenderer>();
                if (sr != null)
                {
                    for (float t = 1f; t >= 0f; t -= Time.deltaTime)
                    {
                        Color c = sr.color;
                        c.a = t;
                        sr.color = c;
                        yield return null;
                    }
                }

                // enemydeath_sfx.SetActive(false);
                Destroy(enemy);
            }
        }
        
        if (enemy_ambient_sfx != null && enemy_ambient_sfx.isPlaying){
            enemy_ambient_sfx.Stop();
        }

        activeEnemies.Clear();
        enemiesKilled = 0;
        UpdateEnemyCounterUI();
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

    void UpdateEnemyCounterUI()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (enemyCounterText != null)
        {
            if (!levelEnd)
            {
                enemyCounterText.text = $"Enemies Left: {enemiesKilled} / {killsNeededForWaveClear}";
            }
            else if (levelEnd)
            {
                if (currentSceneIndex == 4)
                {
                    enemyCounterText.text = $"Level Cleared?";
                } 
                else
                {
                    enemyCounterText.text = $"Level Cleared";
                }
            }
        }
    }

    IEnumerator ClearWaveAfterFullKill()
    {
        yield return StartCoroutine(FadeOutRemainingEnemies());
        enemiesRemaining = 0;
    }
}
