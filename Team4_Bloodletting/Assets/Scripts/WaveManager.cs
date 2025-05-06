using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; //check if need
using System.Collections.Generic;
using Unity.VisualScripting;

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
    //private int kills = 0;
    //private double killsNeeded = 0;
    private bool waveInProgress = false;

    private GameHandler gameHandler;
    public GameObject door;
    public GameObject clearHUD;

    // ENEMY SFX
    public AudioSource enemydeath_sfx;


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

        gameHandler = FindObjectOfType<GameHandler>();
        StartCoroutine(WaitForTutorialThenStart()); // Wait for tutorial/NPC chat to finish
        if (clearHUD != null)
        {
            clearHUD.SetActive(false);
        }
    }

    void Update()
    {
        if(gameHandler.dialogue_complete) {
            if (!waveInProgress && enemiesRemaining == 0)
            {
                // If more waves remain, starat next one
                if (currentWaveIndex + 1 < waves.Count)
                {
                    float delay = waves[currentWaveIndex + 1].waveDelay;
                    Debug.Log($"Wave {currentWaveIndex + 1} complete. Next wave in {delay} seconds.");
                    StartCoroutine(BeginNextWaveAfterDelay(delay));
                }
            
                else
                {
                    //Debug.Log("All waves completed. Advancing to next level.");
                    if (gameHandler != null)
                    { // generalized for all levels
                        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                        int nextSceneIndex = currentSceneIndex + 1;

                        if (gameHandler != null)
                        {
                            if (currentSceneIndex == 4)
                            {
                                GetComponent<HoldenOtherTemp>().enabled = true;
                                clearHUD.SetActive(true);
                            }
                            else
                            {
                                door.GetComponent<Animator>().enabled = true;
                                GetComponent<BoxCollider2D>().enabled = true;
                                clearHUD.SetActive(true);
                            }
                        }

                        // if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
                        // {
                        //     SceneManager.LoadScene(nextSceneIndex);
                        // }
                        else
                        {
                            Debug.Log("No more levels to load. Game complete!");
                        }
                        // gameHandler.GoToNextLevel("Level2"); // "Level2" is added to Build Settings
                    }
                }
            }
        }
        // // end of waves for level transition
        // if (waves.Count == 0) {
        //     wavesComplete = true;
        // }
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

        yield return StartCoroutine(SpawnWave(waves[currentWaveIndex]));
        waveInProgress = false;
    }

    IEnumerator SpawnWave(Wave wave)
    {
        enemiesRemaining = wave.enemyCount;
        //killsNeeded += wave.enemyCount * 0.8;
        
        Debug.Log($"Spawning Wave {currentWaveIndex + 1} with {wave.enemyCount} enemies.");

        for (int i = 0; i < wave.enemyCount; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            // Randomize speed
            EnemyChasePlayer chase = enemy.GetComponent<EnemyChasePlayer>();
            if (chase != null)
            {
                float speed = Random.Range(wave.minSpeedMultiplier, wave.maxSpeedMultiplier);
                chase.SetSpeedMultiplier(speed);
            }

            // Death tracking
            EnemyDeathNotifier notifier = enemy.GetComponent<EnemyDeathNotifier>();
            if (notifier == null)
                notifier = enemy.AddComponent<EnemyDeathNotifier>();
            notifier.manager = this;

            yield return new WaitForSeconds(wave.spawnRate);
        }
    }

    public void OnEnemyDied()
    {
        // play enemy sound!!
        enemydeath_sfx.Play();
        enemiesRemaining--;
        //kills++;
        Debug.Log($"Enemy died. Remaining in wave: {enemiesRemaining}");
    }


    IEnumerator WaitForTutorialThenStart()
    {
        Debug.Log("Waiting for tutorial to complete...");
        // Wait until the GameHandler sets dialogue_complete to true
        while (gameHandler != null && !gameHandler.dialogue_complete)
        {
            yield return null; // Wait one frame and check again
        }

        Debug.Log("Tutorial complete! Starting waves...");
        StartCoroutine(BeginNextWaveAfterDelay(2f));
    }

}
