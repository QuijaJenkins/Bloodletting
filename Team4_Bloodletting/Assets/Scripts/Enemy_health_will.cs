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




using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_health_will : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private GameHandler gameHandler;
    public bool shield;
    private int input;
    private float timer;
    private float maxTime;
    private bool shatter;
    private bool guard;
    public GameObject waveManager;
    public GameObject door;
    public GameObject clearHUD;



    void Start()
    {
        if (gameObject.GetComponent<Renderer>().sortingOrder == 2)
        {
            shield = true;
        }
        currentHealth = maxHealth;
        gameHandler = GameObject.FindObjectOfType<GameHandler>();
        timer = 0;
        input = 0;
        gameObject.GetComponent<Animator>().SetInteger("input", input);
    }

    public void Update()
    { 
        if (shatter)
        {
            if (timer >= maxTime)
            {
                input = 3;
                gameObject.GetComponent<Animator>().SetInteger("input", input);
                timer = 0;
                shatter = false;
            }
            else
            {
                timer++;
            }
        }
        if (guard)
        {
            if (timer >= maxTime)
            {
                input = 0;
                gameObject.GetComponent<Animator>().SetInteger("input", input);
                timer = 0;
                guard = false;
            }
            else
            {
                timer++;
            }
        }
    }
    public void takeDamage(int damage)
    {
        currentHealth -= damage;

        if (gameObject.GetComponent<Renderer>().sortingOrder == 2)
        {
            if (currentHealth <= 0 && shield)
            {
                currentHealth = maxHealth/2;
                shield = false;
                Shatter(110);
            }
            else if(shield)
            {
                Guard(150);
            }
        }
        if (currentHealth <= 0)
        {
            Die();
            if(tag == "enemyShooter") {
                Debug.Log("yay");
                waveManager.GetComponent<BoxCollider2D>().enabled = true;
                door.GetComponent<Animator>().enabled = true;
                clearHUD.SetActive(true);
            }
            
        }
    }

    void Die()
    {
        Destroy(gameObject);
        if (GameHandler.hard)
        {
            gameHandler.changeHealth(15, false);
        }
        else
        {
            gameHandler.changeHealth(20, false);
        }
        gameHandler.xp += 10;
    }


    //sets animation to walk after shatter
    void Shatter(float time)
    {
        input = 2;
        gameObject.GetComponent<Animator>().SetInteger("input", input);
        maxTime = time;
        shatter = true;
    }

    //sets animation to guard on hit
    void Guard(float time)
    {
        input = 1;
        gameObject.GetComponent<Animator>().SetInteger("input", input);
        maxTime = time;
        guard = true;
    }
}
