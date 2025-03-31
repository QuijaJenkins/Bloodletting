using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.ReorderableList;
using System.Threading;
using UnityEngine.UIElements;

public class GameHandler : MonoBehaviour
{
    private GameObject player;
    public int playerHealth = 100;
    public int StartPlayerHealth = 100;
    public UnityEngine.UI.Image healthBar;
    //state variables
    public string state;
    public int stanceNumber;
    public bool moving;
    public bool stateLocked = false;
    // public GameObject healthText;

    private string sceneName;

    
    // Start is called before the first frame update
    void Start(){
        //start in first stance
        stanceNumber = 1;

        player = GameObject.FindWithTag("Player");
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName=="MainMenu"){ //uncomment these two lines when the MainMenu exists
            playerHealth = StartPlayerHealth;
        }
        updateStatsDisplay();

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = playerHealth / 100f;

        if (Input.GetKeyDown(KeyCode.E) && stanceNumber == 1) { 
            Debug.Log("Entering stance 3");
            stanceNumber = 3;
        } else if (Input.GetKeyDown(KeyCode.E) && stanceNumber == 3) {
            Debug.Log("Entering stance 1");
            stanceNumber = 1;
        }


    }

    private void FixedUpdate()
    {
        //Determines your animation based on your current state
        if (!stateLocked)
        {
            //gets movement direction and prioritizes the first key pressed
            if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") == 0)
            {
                state = "bWalk";
            }
            if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") == 0)
            {
                state = "fWalk";
            }
            if (Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") == 0)
            {
                state = "dWalk";
            }
            if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") == 0)
            {
                state = "uWalk";
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StateLock(10);
                state = "dashing";
            }
            if (!Input.anyKey)
            {
                state = "idle";
            }
        }
    }

    public void updateStatsDisplay(){
       
       //ADD BELOW WHEN READY --> Displays player's health
        // Text healthTextTemp = healthText.GetComponent<Text>();
        // healthTextTemp.text = "HEALTH: " + playerHealth;

    }

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


    public void RestartGame() {
        Time.timeScale = 1f;
        GameHandler_PauseMenu.GameisPaused = false;
        SceneManager.LoadScene("MainMenu");
            // Please also reset all static variables here, for new games!
        playerHealth = StartPlayerHealth;
    }

    public void QuitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void StartGame() {
            SceneManager.LoadScene("Level1");
    }

    public void LoadMainMenu() {
            SceneManager.LoadScene("MainMenu");
    }

    
    public void Credits() {
        SceneManager.LoadScene("CreditScene");
    }

    public void StateLock(double waitTime)
    {
        double wait = 0;
        stateLocked = true;
        if (wait > waitTime)
        {
            stateLocked = false;
            wait = 0;
        }else{
            wait++;
        }
    }
}
