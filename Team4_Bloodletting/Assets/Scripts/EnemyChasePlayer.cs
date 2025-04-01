// // SWARM OF ENEMIES CHASING ALL AT ONCE

// using UnityEngine;

// public class EnemyChasePlayer : MonoBehaviour
// {
//     public float minChaseSpeed = 1f;
//     public float maxChaseSpeed = 3f;
//     public float chaseProbability = 0.4f;
//     public float guardProbability = 0.2f; // 20% of enemies will guard

//     private Transform player;
//     private float chaseSpeed;
//     private bool isChasing = false;
//     private bool isGuarding = false;
//     private EnemyIdleBounce idleBounce;

//     void Start()
//     {
//         player = GameObject.FindGameObjectWithTag("Player")?.transform;
//         chaseSpeed = Random.Range(minChaseSpeed, maxChaseSpeed);
//         idleBounce = GetComponent<EnemyIdleBounce>();

//         // Decide behavior only if chasing is allowed
//         if (EnemySpawner.chasingAllowed)
//         {
//             float rand = Random.value;
//             if (rand < guardProbability)
//             {
//                 isGuarding = true;
//             }
//             else if (rand < guardProbability + chaseProbability)
//             {
//                 isChasing = true;
//             }
//         }
//     }

//     void Update()
//     {
//         // Late activation when global chase begins
//         if (!isChasing && !isGuarding && EnemySpawner.chasingAllowed)
//         {
//             float rand = Random.value;
//             if (rand < guardProbability)
//             {
//                 isGuarding = true;
//             }
//             else if (rand < guardProbability + chaseProbability)
//             {
//                 isChasing = true;
//             }
//         }

//         if (isChasing && player != null)
//         {
//             if (idleBounce != null) Destroy(idleBounce);
//             Vector2 dir = (player.position - transform.position).normalized;
//             transform.Translate(dir * chaseSpeed * Time.deltaTime);
//         }

//         // If guarding: do nothing (or we could add a bounce or idle animation)
//     }
// }

// using UnityEngine;

// public class EnemyChasePlayer : MonoBehaviour
// {
//     public float minChaseSpeed = 1f;
//     public float maxChaseSpeed = 3f;
//     public float chaseProbability = 0.4f;
//     public float guardProbability = 0.2f;

//     private Transform player;
//     private float chaseSpeed;
//     private bool isChasing = false;
//     private bool isGuarding = false;
//     private EnemyIdleBounce idleBounce;

//     void Start()
//     {
//         player = GameObject.FindGameObjectWithTag("Player")?.transform;
//         chaseSpeed = Random.Range(minChaseSpeed, maxChaseSpeed);
//         idleBounce = GetComponent<EnemyIdleBounce>();

//         if (!isGuarding && EnemySpawner.chasingAllowed)
//         {
//             float rand = Random.value;
//             if (rand < guardProbability)
//             {
//                 isGuarding = true;
//             }
//             else if (rand < guardProbability + chaseProbability)
//             {
//                 isChasing = true;
//             }
//         }
//     }

//     void Update()
//     {
//         // If not already set and chasing is now allowed, make a decision
//         if (!isGuarding && !isChasing && EnemySpawner.chasingAllowed)
//         {
//             float rand = Random.value;
//             if (rand < guardProbability)
//             {
//                 isGuarding = true;
//             }
//             else if (rand < guardProbability + chaseProbability)
//             {
//                 isChasing = true;
//             }
//         }

//         if (isGuarding)
//         {
//             // Stay put, maybe bounce or animate
//             return;
//         }

//         if (isChasing && player != null)
//         {
//             if (idleBounce != null) Destroy(idleBounce);
//             Vector2 direction = (player.position - transform.position).normalized;
//             transform.Translate(direction * chaseSpeed * Time.deltaTime);
//         }
//     }

//     public void SetAsGuard()
//     {
//         isGuarding = true;
//         isChasing = false;

//         // Optional: mark visually
//         GetComponent<SpriteRenderer>().color = Color.red;
//     }
// }

using UnityEngine;

public class EnemyChasePlayer : MonoBehaviour
{
    public float minChaseSpeed = 1.2f;
    public float maxChaseSpeed = 4.5f;
    public float chaseProbability = 0.4f;
    public float guardProbability = 0.2f;

    private Transform player;
    private float chaseSpeed;
    private bool isChasing = false;
    private bool isGuarding = false;
    private EnemyIdleBounce idleBounce;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        chaseSpeed = Random.Range(minChaseSpeed, maxChaseSpeed);
        idleBounce = GetComponent<EnemyIdleBounce>();

        // Only decide behavior if this is not a guard and chasing is allowed
        if (!isGuarding && EnemySpawner.chasingAllowed)
        {
            float rand = Random.value;
            if (rand < guardProbability)
            {
                isGuarding = true;
            }
            else if (rand < guardProbability + chaseProbability)
            {
                isChasing = true;
            }
        }

        // 🔍 Optional: color-code chasers by speed
        if (isChasing)
        {
            float t = Mathf.InverseLerp(minChaseSpeed, maxChaseSpeed, chaseSpeed);
            GetComponent<SpriteRenderer>().color = Color.Lerp(Color.yellow, Color.magenta, t);
            Debug.Log("Enemy will chase at speed: " + chaseSpeed);
        }
    }

    void Update()
    {
        // Late behavior decision once chase is allowed
        if (!isGuarding && !isChasing && EnemySpawner.chasingAllowed)
        {
            float rand = Random.value;
            if (rand < guardProbability)
            {
                isGuarding = true;
            }
            else if (rand < guardProbability + chaseProbability)
            {
                isChasing = true;

                // Optional: color based on speed again here (for enemies that become chasers later)
                float t = Mathf.InverseLerp(minChaseSpeed, maxChaseSpeed, chaseSpeed);
                GetComponent<SpriteRenderer>().color = Color.Lerp(Color.yellow, Color.magenta, t);
                Debug.Log("Late chaser activated at speed: " + chaseSpeed);
            }
        }

        if (isGuarding)
        {
            return; // Do nothing
        }

        if (isChasing && player != null)
        {
            if (idleBounce != null) Destroy(idleBounce);
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * chaseSpeed * Time.deltaTime);
        }
    }

    public void SetAsGuard()
    {
        isGuarding = true;
        isChasing = false;
        GetComponent<SpriteRenderer>().color = Color.red; // Visual for guards
    }
}
