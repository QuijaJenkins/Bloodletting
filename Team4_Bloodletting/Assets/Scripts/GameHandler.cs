using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    private GameObject player;
    public static int playerHealth = 100;
    public int StartPlayerHealth = 100;
    // public GameObject healthText;

    private string sceneName;

    
    // Start is called before the first frame update
    void Start(){
        player = GameObject.FindWithTag("Player");
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName=="MainMenu"){ //uncomment these two lines when the MainMenu exists
            playerHealth = StartPlayerHealth;
        }
        updateStatsDisplay();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void updateStatsDisplay(){
       
       //ADD BELOW WHEN READY --> Displays player's health
        // Text healthTextTemp = healthText.GetComponent<Text>();
        // healthTextTemp.text = "HEALTH: " + playerHealth;

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


}
