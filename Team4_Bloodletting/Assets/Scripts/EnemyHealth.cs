// using UnityEngine;

// public class EnemyHealth : MonoBehaviour
// {
//     public int maxHealth = 2;
//     private int currentHealth;

//     private SpriteRenderer spriteRenderer;
//     private Vector3 originalPosition;
//     private bool isShaking = false;

//     void Start()
//     {
//         currentHealth = maxHealth;
//         spriteRenderer = GetComponent<SpriteRenderer>();
//         originalPosition = transform.position;
//     }

//     public void TakeDamage()
//     {
//         currentHealth--;

//         if (currentHealth == 1)
//         {
//             FlashRed();
//             StartCoroutine(Shake(0.2f, 0.1f));
//         }
//         else if (currentHealth <= 0)
//         {
//             Die();
//         }
//     }

//     void FlashRed()
//     {
//         if (spriteRenderer != null)
//         {
//             spriteRenderer.color = Color.red;
//             Invoke(nameof(ResetColor), 0.2f);
//         }
//     }

//     void ResetColor()
//     {
//         if (spriteRenderer != null)
//             spriteRenderer.color = Color.white;
//     }

//     System.Collections.IEnumerator Shake(float duration, float strength)
//     {
//         isShaking = true;
//         float timer = 0f;

//         while (timer < duration)
//         {
//             transform.position = originalPosition + (Vector3)Random.insideUnitCircle * strength;
//             timer += Time.deltaTime;
//             yield return null;
//         }

//         transform.position = originalPosition;
//         isShaking = false;
//     }

//     void Die()
//     {
//         Destroy(gameObject);
//     }
// }

// using UnityEngine;

// public class EnemyHealth : MonoBehaviour
// {
//     public int maxHealth = 2;
//     private int currentHealth;

//     private SpriteRenderer spriteRenderer;
//     private Vector3 originalPosition;
//     private Rigidbody2D rb;
//     private bool isShaking = false;

//     void Start()
//     {
//         currentHealth = maxHealth;
//         spriteRenderer = GetComponent<SpriteRenderer>();
//         originalPosition = transform.position;
//         rb = GetComponent<Rigidbody2D>(); // get Rigidbody2D for knockback
//     }

//     public void TakeDamage()
//     {
//         currentHealth--;

//         if (currentHealth == 1)
//         {
//             FlashRed();
//             StartCoroutine(Shake(0.15f, 0.1f));
//             ApplyKnockback(); 
//         }
//         else if (currentHealth <= 0)
//         {
//             Die();
//         }
//     }

//     void FlashRed()
//     {
//         if (spriteRenderer != null)
//         {
//             spriteRenderer.color = Color.red;
//             Invoke(nameof(ResetColor), 0.2f);
//         }
//     }

//     void ResetColor()
//     {
//         if (spriteRenderer != null)
//             spriteRenderer.color = Color.white;
//     }

//     System.Collections.IEnumerator Shake(float duration, float strength)
//     {
//         isShaking = true;
//         float timer = 0f;

//         while (timer < duration)
//         {
//             transform.position = originalPosition + (Vector3)Random.insideUnitCircle * strength;
//             timer += Time.deltaTime;
//             yield return null;
//         }

//         transform.position = originalPosition;
//         isShaking = false;
//     }

//     void ApplyKnockback()
//     {
//         if (rb != null)
//         {
//             Vector2 knockbackDir = (transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).normalized;
//             rb.AddForce(knockbackDir * 200f); // tweak this value for drama
//         }
//     }

//     void Die()
//     {
//         Destroy(gameObject);
//     }
// }

// using UnityEngine;

// public class EnemyHealth : MonoBehaviour
// {
//     public int maxHealth = 2;
//     private int currentHealth;

//     private SpriteRenderer spriteRenderer;
//     private Rigidbody2D rb;
//     private float lastHitTime = -999f;
//     public float hitCooldown = 0.3f;

//     void Start()
//     {
//         currentHealth = maxHealth;
//         spriteRenderer = GetComponent<SpriteRenderer>();
//         rb = GetComponent<Rigidbody2D>();
//     }

//     public void TakeDamage()
//     {
//         if (Time.time - lastHitTime < hitCooldown) return; // Prevent rapid double-hits
//         lastHitTime = Time.time;

//         currentHealth--;

//         if (currentHealth == 1)
//         {
//             FlashRed();
//             StartCoroutine(Shake(0.15f, 0.1f));
//             ApplyKnockback();
//         }
//         else if (currentHealth <= 0)
//         {
//             Die();
//         }
//     }

//     void FlashRed()
//     {
//         if (spriteRenderer != null)
//         {
//             spriteRenderer.color = Color.red;
//             Invoke(nameof(ResetColor), 0.2f);
//         }
//     }

//     void ResetColor()
//     {
//         if (spriteRenderer != null)
//             spriteRenderer.color = Color.white;
//     }

//     System.Collections.IEnumerator Shake(float duration, float strength)
//     {
//         Vector3 originalPosition = transform.position;
//         float timer = 0f;

//         while (timer < duration)
//         {
//             transform.position = originalPosition + (Vector3)Random.insideUnitCircle * strength;
//             timer += Time.deltaTime;
//             yield return null;
//         }

//         transform.position = originalPosition;
//     }

//     void ApplyKnockback()
//     {
//         if (rb != null)
//         {
//             Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
//             if (player == null) return;

//             Vector2 baseDir = (transform.position - player.position).normalized;
//             Vector2 randomOffset = Random.insideUnitCircle * 0.3f;
//             Vector2 knockbackDir = (baseDir + randomOffset).normalized;

//             rb.velocity = Vector2.zero;
//             rb.AddForce(knockbackDir * 200f, ForceMode2D.Impulse);
//         }
//     }

//     void Die()
//     {
//         Destroy(gameObject);
//     }
// }

// using UnityEngine;

// public class EnemyHealth : MonoBehaviour
// {
//     private int health = 2;
//     private Rigidbody2D rb;
//     private float lastHitTime = -999f;
//     public float hitCooldown = 0.25f;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//     }

//     public void TakeDamage()
//     {
//         if (Time.time - lastHitTime < hitCooldown) return; // prevent multiple hits
//         lastHitTime = Time.time;

//         health--;

//         if (health == 1)
//         {
//             KnockbackFromPlayer(); // first hit reaction
//         }
//         else if (health <= 0)
//         {
//             Die(); // second hit = death
//         }
//     }

//     void KnockbackFromPlayer()
//     {
//         if (rb == null) return;
//         Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;

//         // Transform player = GameObject.FindGameObjectWithTag("Player");
//         if (player == null) return;

//         Vector2 dir = (transform.position - player.position).normalized;
//         rb.velocity = Vector2.zero;
//         rb.AddForce(dir * 300f, ForceMode2D.Impulse); // 🔧 you can increase this if needed
//     }

//     void Die()
//     {
//         Destroy(gameObject);
//     }
// }

// using UnityEngine;

// public class EnemyHealth : MonoBehaviour
// {
//     private int health = 2;
//     private Rigidbody2D rb;
//     private float lastHitTime = -999f;
//     public float hitCooldown = 0.25f;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//     }

//     public void TakeDamage()
//     {
//         if (Time.time - lastHitTime < hitCooldown) return;
//         lastHitTime = Time.time;

//         health--;

//         if (health == 1)
//         {
//             KnockbackFromPlayer(); // ✅ First hit: knockback only
//         }
//         else if (health <= 0)
//         {
//             Die(); // ✅ Second hit: destroy
//         }
//     }

//     void KnockbackFromPlayer()
//     {
//         if (rb == null) return;

//         Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
//         if (player == null) return;

//         Vector2 dir = (transform.position - player.position).normalized;
//         rb.velocity = Vector2.zero; // reset velocity before applying force
//         rb.AddForce(dir * 300f, ForceMode2D.Impulse); // 💥 Launch backward
//     }

//     void Die()
//     {
//         Destroy(gameObject);
//     }
// }

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
            KnockbackFromPlayer(); // ✅ First hit: fly back
        }
        else if (health <= 0)
        {
            Die(); // ✅ Second hit: destroy
        }
    }

    void KnockbackFromPlayer()
    {
        if (rb == null) return;

        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null) return;

        Vector2 dir = (transform.position - player.position).normalized;
        rb.velocity = Vector2.zero;
        rb.AddForce(dir * 300f, ForceMode2D.Impulse); // 💥 Fly back on first hit
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

