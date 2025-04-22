// using UnityEngine;

// public class EnemyChasePlayer : MonoBehaviour
// {
//     public float speed = 3f;
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
        
//         if (direction.x > 0)
//         {
//             gameObject.GetComponent<SpriteRenderer>().flipX = true;
//         } else if (direction.x < 0)
//         {
//             gameObject.GetComponent<SpriteRenderer>().flipX = false;
//         }
//     }
// }

using UnityEngine;

public class EnemyChasePlayer : MonoBehaviour
{
    public float baseSpeed = 3f;
    private float speedMultiplier = 1f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        float finalSpeed = baseSpeed * speedMultiplier;

        transform.Translate(direction * finalSpeed * Time.deltaTime);

        if (direction.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (direction.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    // Called by WaveManager to scale difficulty
    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }
}
