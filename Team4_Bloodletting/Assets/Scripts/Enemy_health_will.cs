// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Enemy_health_will : MonoBehaviour
// {
//     static float maxHealth = 100;
//     float currHealth;
//     private GameHandler gameHandler;

//     // Start is called before the first frame update
//     void Start()
//     {
//         currHealth = maxHealth; 
//         gameHandler = GameObject.FindObjectOfType<GameHandler>();
   
//     }

//     public void takeDamage(float damage) {

//         currHealth -= damage;

//         if (currHealth <= 0) {
//             die();
//         }
//     }

//     void die(){
//         Destroy(gameObject);
//         gameHandler.changeHealth(20, false);
//         gameHandler.xp += 10;
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }

// using UnityEngine;

// public class Enemy_health_will : MonoBehaviour
// {
//     public int maxHealth = 100;
//     public int currentHealth;

//     void Start()
//     {
//         currentHealth = maxHealth;
//     }

//     public void takeDamage(int damage)
//     {
//         currentHealth -= damage;

//         if (currentHealth <= 0)
//         {
//             Die();
//         }
//     }

//     void Die()
//     {
//         // You can add death animation or sound here if you want
//         Destroy(gameObject);
//     }
// }




using UnityEngine;

public class Enemy_health_will : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private GameHandler gameHandler;

    void Start()
    {
        currentHealth = maxHealth;
        gameHandler = GameObject.FindObjectOfType<GameHandler>();
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        gameHandler.changeHealth(20, false);
        gameHandler.xp += 10;
    }
}
