using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int health = 2;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage()
    {
        health--;

        if (health == 1)
        {
            KnockbackFromPlayer(); 
        }
        else if (health <= 0)
        {
            Die(); 
        }
    }

    void KnockbackFromPlayer()
    {
        if (rb == null) return;

        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null) return;

        Vector2 dir = (transform.position - player.position).normalized;
        rb.velocity = Vector2.zero;
        rb.AddForce(dir * 300f, ForceMode2D.Impulse); 
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

