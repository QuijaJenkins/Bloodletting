using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_health_will : MonoBehaviour
{
    static int maxHealth = 100;
    int currHealth;
    private GameHandler gameHandler;



    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth; 
        gameHandler = GameObject.FindObjectOfType<GameHandler>();
   
    }

    public void takeDamage(int damage) {

        currHealth -= damage;

        if (currHealth <= 0) {
            die();
        }
    }

    void die(){
        Destroy(gameObject);
        gameHandler.changeHealth(20, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
