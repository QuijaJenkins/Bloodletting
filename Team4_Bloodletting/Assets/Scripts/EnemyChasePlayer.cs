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

