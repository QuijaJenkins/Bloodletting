// using UnityEngine;

// public class EnemyChasePlayer : MonoBehaviour
// {
//     public float minChaseSpeed = 1.2f;
//     public float maxChaseSpeed = 4.5f;
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

//         // Only decide behavior if this is not a guard and chasing is allowed
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

//         // Optional: color-code chasers by speed
//         if (isChasing)
//         {
//             float t = Mathf.InverseLerp(minChaseSpeed, maxChaseSpeed, chaseSpeed);
//             GetComponent<SpriteRenderer>().color = Color.Lerp(Color.yellow, Color.magenta, t);
//             Debug.Log("Enemy will chase at speed: " + chaseSpeed);
//         }
//     }

//     void Update()
//     {
//         // Late behavior decision once chase is allowed
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

//                 // Optional: color based on speed again here (for enemies that become chasers later)
//                 float t = Mathf.InverseLerp(minChaseSpeed, maxChaseSpeed, chaseSpeed);
//                 GetComponent<SpriteRenderer>().color = Color.Lerp(Color.yellow, Color.magenta, t);
//                 Debug.Log("Late chaser activated at speed: " + chaseSpeed);
//             }
//         }

//         if (isGuarding)
//         {
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
//         GetComponent<SpriteRenderer>().color = Color.red; // Visual for guards
//     }
// }

// using UnityEngine;

// public class EnemyChasePlayer : MonoBehaviour
// {
//     public float minChaseSpeed = 1.2f;
//     public float maxChaseSpeed = 4.5f;

//     private Transform player;
//     private float chaseSpeed;

//     void Start()
//     {
//         player = GameObject.FindGameObjectWithTag("Player")?.transform;
//         chaseSpeed = Random.Range(minChaseSpeed, maxChaseSpeed);
//     }

//     void Update()
//     {
//         if (player != null)
//         {
//             Vector2 direction = (player.position - transform.position).normalized;
//             transform.Translate(direction * chaseSpeed * Time.deltaTime);
//         }
//     }
// }


// using UnityEngine;

// public class EnemyChasePlayer : MonoBehaviour
// {
//     public float minSpeed = 1.5f;
//     public float maxSpeed = 4.5f;

//     private Transform player;
//     private float speed;

//     void Start()
//     {
//         player = GameObject.FindGameObjectWithTag("Player")?.transform;

//         if (player == null)
//         {
//             Debug.LogError("Player not found! Make sure the player is tagged 'Player'");
//             enabled = false;
//             return;
//         }

//         speed = Random.Range(minSpeed, maxSpeed);
//     }

//     void Update()
//     {
//         if (player == null) return;

//         Vector2 direction = (player.position - transform.position).normalized;
//         transform.Translate(direction * speed * Time.deltaTime);
//     }
// }

// using UnityEngine;

// public class EnemyChasePlayer : MonoBehaviour
// {
//     public float speed = 6f;
//     private Transform player;

//     void Start()
//     {
//         player = GameObject.FindGameObjectWithTag("Player")?.transform;
//     }

//     void Update()
//     {
//         if (player == null) return;

//         Vector2 direction = (player.position - transform.position).normalized;
//         transform.Translate(direction * speed * Time.deltaTime);
//     }
// }

using UnityEngine;

public class EnemyChasePlayer : MonoBehaviour
{
    public float speed = 3f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
        
        if (direction.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        } else if (direction.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}

