// using UnityEngine;

// public class EnemyHealth : MonoBehaviour
// {
//     public int maxHealth = 3;
//     private int currentHealth;

//     void Start()
//     {
//         currentHealth = maxHealth;
//     }

//     public void TakeDamage(int damage)
//     {
//         currentHealth -= damage;

//         if (currentHealth <= 0)
//         {
//             Die();
//         }
//     }

//     void Die()
//     {
//         // Option 1: destroy the object
//         Destroy(gameObject);

//         // Option 2: swap to dead sprite instead of destroy (if you want visuals later)
//         // GetComponent<SpriteRenderer>().sprite = deadSprite;
//     }
// }

using System.Collections;
using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    private int health = 2;
    private bool isHitOnce = false;

    public void TakeHit()
    {
        health--;

        if (health == 1 && !isHitOnce)
        {
            isHitOnce = true;
            StartCoroutine(Shake());
        }
        else if (health <= 0)
        {
            Die();
        }
    }

    IEnumerator Shake()
    {
        Vector3 originalPos = transform.position;
        float duration = 0.2f;
        float strength = 0.1f;
        float timer = 0f;

        while (timer < duration)
        {
            transform.position = originalPos + (Vector3)Random.insideUnitCircle * strength;
            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}