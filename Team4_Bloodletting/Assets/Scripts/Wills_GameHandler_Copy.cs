using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Wills_GameHandler_Copy : MonoBehaviour
{
    private GameObject player;
    public int playerHealth = 100;
    public int StartPlayerHealth = 100;
    public Image healthBar;
    
    // public GameObject healthText;

    private string sceneName;

    // Start is called before the first frame update
    void Start(){
        player = GameObject.FindWithTag("Player");
        sceneName = SceneManager.GetActiveScene().name;
        //if (sceneName=="MainMenu"){ //uncomment these two lines when the MainMenu exists
            playerHealth = StartPlayerHealth;
        //}
        updateStatsDisplay();
    }

    // Update is called once per frame
    void Update() {
        healthBar.fillAmount = playerHealth / 100f;
    }

    public void updateStatsDisplay(){
       
       //ADD BELOW WHEN READY --> Displays player's health
        // Text healthTextTemp = healthText.GetComponent<Text>();
        // healthTextTemp.text = "HEALTH: " + playerHealth;

    }

    //Change player health by given integer. Positive heals, negative means damage
    //
    public void changeHealth(int healthChange, bool playerAttack) {

        //health can't go over 100
        if (playerHealth + healthChange >= 100) {
            playerHealth = 100;
        }
        else{
            //player cannot kill themself by attack, leave them on 1hp
            if (playerAttack && playerHealth + healthChange <= 0) {
                playerHealth = 1;
            }
            else {
                playerHealth += healthChange;
            }
        }
    }
}
