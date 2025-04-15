// using UnityEngine;

// public class EnemyDamagePlayer : MonoBehaviour
// {
//     public int damageAmount = 15;
//     private bool hasDamagedPlayer = false;

//     void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (!hasDamagedPlayer && collision.gameObject.CompareTag("Player"))
//         {
//             GameHandler handler = FindObjectOfType<GameHandler>();
//             if (handler != null)
//             {
//                 handler.changeHealth(-damageAmount, false);
//                 hasDamagedPlayer = true;

//                 Destroy(gameObject); // optional, you can remove this if you want enemies to stay
//             }
//         }
//     }
// }

// using UnityEngine;
// using System.Collections;

// public class EnemyDamagePlayer : MonoBehaviour
// {
//     public int damageAmount = 15;
//     public float cooldownTime = 1.2f;
//     private bool canDamagePlayer = true;

//     void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (canDamagePlayer && collision.gameObject.CompareTag("Player"))
//         {
//             GameHandler handler = FindObjectOfType<GameHandler>();
//             if (handler != null)
//             {
//                 handler.changeHealth(-damageAmount, false);
//             }

//             StartCoroutine(DamageCooldown());
//         }
//     }

//     IEnumerator DamageCooldown()
//     {
//         canDamagePlayer = false;

//         // Optional: flash gray to indicate cooldown
//         SpriteRenderer sr = GetComponent<SpriteRenderer>();
//         if (sr != null)
//         {
//             Color originalColor = sr.color;
//             sr.color = Color.gray;
//             yield return new WaitForSeconds(0.2f);
//             sr.color = originalColor;
//         }

//         // Wait for cooldown before allowing damage again
//         yield return new WaitForSeconds(cooldownTime);
//         canDamagePlayer = true;
//     }
// }

// using UnityEngine;
// using System.Collections;

// public class EnemyDamagePlayer : MonoBehaviour
// {
//     public int damageAmount = 15;
//     public float cooldownTime = 1.2f;
//     private bool canDamagePlayer = true;
//     private SpriteRenderer spriteRenderer;

//     void Start()
//     {
//         spriteRenderer = GetComponent<SpriteRenderer>();
//     }

//     void OnTriggerEnter2D(Collider2D other)
//     {
//         if (canDamagePlayer && other.CompareTag("Player"))
//         {
//             GameHandler handler = FindObjectOfType<GameHandler>();
//             if (handler != null)
//             {
//                 handler.changeHealth(-damageAmount, false);
//                 StartCoroutine(DamageCooldown());
//             }
//         }
//     }

//     IEnumerator DamageCooldown()
//     {
//         canDamagePlayer = false;

//         // Optional flicker
//         if (spriteRenderer != null)
//         {
//             spriteRenderer.color = Color.gray;
//             yield return new WaitForSeconds(0.2f);
//             spriteRenderer.color = Color.white;
//         }

//         yield return new WaitForSeconds(cooldownTime - 0.2f);
//         canDamagePlayer = true;
//     }
// }

using UnityEngine;
using System.Collections;

public class EnemyDamagePlayer : MonoBehaviour
{
    public int damageAmount = 15;
    public float cooldownTime = 1.2f;
    private bool canDamagePlayer = true;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (canDamagePlayer && other.CompareTag("Player"))
        {
            GameHandler handler = FindObjectOfType<GameHandler>();
            if (handler != null)
            {
                handler.changeHealth(-damageAmount, false);
                StartCoroutine(DamageCooldown());
            }
        }
    }

    IEnumerator DamageCooldown()
    {
        canDamagePlayer = false;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.gray;
        }

        yield return new WaitForSeconds(0.15f); // flicker duration

        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor; // reset color exactly
        }

        yield return new WaitForSeconds(cooldownTime - 0.15f); // rest of cooldown
        canDamagePlayer = true;
    }
}
