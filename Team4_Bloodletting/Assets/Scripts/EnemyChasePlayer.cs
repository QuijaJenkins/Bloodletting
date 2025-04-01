using UnityEngine;

public class EnemyChasePlayer : MonoBehaviour
{
    public float minChaseSpeed = 1f;
    public float maxChaseSpeed = 3f;
    public float chaseProbability = 0.4f; // 40% of enemies will chase

    private Transform player;
    private float chaseSpeed;
    private bool isChasing = false;
    private EnemyIdleBounce idleBounce;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        chaseSpeed = Random.Range(minChaseSpeed, maxChaseSpeed);

        // Decide if this enemy will chase
        if (Random.value < chaseProbability)
        {
            isChasing = true;
        }

        idleBounce = GetComponent<EnemyIdleBounce>();
    }

    void Update()
    {
        if (isChasing && player != null)
        {
            // Stop bouncing once it starts chasing
            if (idleBounce != null)
                Destroy(idleBounce);

            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * chaseSpeed * Time.deltaTime);
        }
    }
}
