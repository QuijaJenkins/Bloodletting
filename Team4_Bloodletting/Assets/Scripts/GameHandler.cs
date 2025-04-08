using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using Unity.VisualScripting.ReorderableList;
using System.Threading;
using UnityEngine.UIElements;

public class GameHandler : MonoBehaviour
{
    private GameObject player;
    public int playerHealth = 100;
    public int StartPlayerHealth = 100;
    public UnityEngine.UI.Image healthBar;
    //state variables
    public Vector2 movement;
    public Animator playerAnim;
    public int spr_dir = 2;
    public int stanceNumber;
    public bool moving;
    public int input = 0;
    public int currentInput = 0;
    public bool inputActive = true;
    public double wait = 0;
    public double lockTimer = 0;
    private bool testTrigger;

    //Variables for switching attack scripts based on stance
    public PlayerAttackMelee meleeScript;
    public PlayerMoveAimShoot projectileScript;

    /*DON'T DELETE; reference for all the animation states
    public int idle = 0;
    public int uWalk = 1;
    public int fWalk = 2;
    public int dWalk = 3;
    public int bWalk = 4;
    public int dashing = 8;
    public int uAttack = 9;
    public int fAttack = 10;
    public int dAttack = 11;
    public int stanceSwap1 = 12; //just one swap needed?
    public int stanceSwap2 = 13;
    public int stanceSwap3 = 14;*/

    // public GameObject healthText;

    private string sceneName;

    
    // Start is called before the first frame update
    void Start(){
        //start in first stance
        stanceNumber = 1;
        updateAttackScriptByStance();


        player = GameObject.FindWithTag("Player");
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName=="MainMenu"){ //uncomment these two lines when the MainMenu exists
            playerHealth = StartPlayerHealth;
        }
        playerAnim = player.GetComponentInChildren<Animator>();
        updateStatsDisplay();
        spr_dir = 2;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = playerHealth / 100f;

        //code for stance switching. Click 1 2 or 3 for respective stance
        if (Input.GetKeyDown(KeyCode.Alpha1) && stanceNumber != 1) { 
            Debug.Log("Entering stance 1");
            stanceNumber = 1;
            updateAttackScriptByStance();
        } else if (Input.GetKeyDown(KeyCode.Alpha2) && stanceNumber != 2) {
            Debug.Log("Entering stance 2");
            stanceNumber = 2;
            updateAttackScriptByStance();
        } else if (Input.GetKeyDown(KeyCode.Alpha3) && stanceNumber != 3) {
            Debug.Log("Entering stance 3");
            stanceNumber = 3;
            updateAttackScriptByStance();
        }

        movement = player.GetComponent<playerMove>().movement;


        //unlocks lock after set time
        if (!inputActive)
        {  
            lockTimer += Time.deltaTime;
            if (lockTimer > wait)
            {
                inputActive = true;
                lockTimer -= wait;
            }
        }
            //Debug.Log(spr_dir);
        //current state code
        if (spr_dir == 4)
        {
            player.GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        else
        {
            player.GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        if (inputActive)
        {
            //gets movement direction via movement vector
            if (!Input.anyKey)
            {
                if (input != 0)
                {
                    Trigger();
                }
                input = 0;
                playerAnim.SetInteger("input", input);
            }

            if (movement.y > Mathf.Abs(movement.x))
            {
                if (input != 1)
                {
                    Trigger();
                }
                input = 1;
                playerAnim.SetInteger("input", input);
                spr_dir = 1;
                playerAnim.SetInteger("spr_dir", spr_dir);
            }

            if (movement.x > Mathf.Abs(movement.y))
            {
                if (input != 2)
                {
                    Trigger();
                }
                input = 2;
                playerAnim.SetInteger("input", input);
                spr_dir = 2;
                playerAnim.SetInteger("spr_dir", spr_dir);
            }

            if (movement.y < -Mathf.Abs(movement.x))
            {
                if (input != 3)
                {
                    Trigger();
                }
                input = 3;
                playerAnim.SetInteger("input", input);
                spr_dir = 3;
                playerAnim.SetInteger("spr_dir", spr_dir);
            }

            if (movement.x < -Mathf.Abs(movement.y))
            {
                //flip around
                if (input != 4)
                {
                    Trigger();
                }
                input = 4;
                playerAnim.SetInteger("input", input);
                spr_dir = 4;
                playerAnim.SetInteger("spr_dir", spr_dir);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (input != 0)
                {
                    Trigger();
                }
                input = 5;
                playerAnim.SetInteger("input", input);
                InputLock(10);
                //dashing = true;
            }

            //sets attack state based on current stance and direction
            //I haven't implemented combo attacks yet
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //this needs to be removed and added with attack window
                if (spr_dir == 2)
                {
                    if (stanceNumber == 1)
                    {
                        if (input != 6)
                        {
                            Trigger();
                        }
                        input = 6;
                        playerAnim.SetInteger("input", input);
                        InputLock(0.25);
                    }       
                     //if (stanceNumber == 2) { }

                     if (stanceNumber == 3)
                     {
                        InputLock(4);
                     }          
                 }
                if (spr_dir == 4) {
                    if (stanceNumber == 1)
                    {
                        if (input != 6)
                        {
                            Trigger();
                        }
                        input = 6;
                        playerAnim.SetInteger("input", input);
                        InputLock(0.25);
                    }
                    //if (stanceNumber == 2){}

                    if (stanceNumber == 3)
                    {
                        InputLock(4);
                    }
                }
                if(spr_dir == 1) 
                {
                    if (stanceNumber == 1)
                    {
                        Debug.Log("hmm");
                        if (input != 6)
                        {
                            Trigger();
                        }
                        input = 6;
                        playerAnim.SetInteger("input", input);
                        InputLock(0.25);
                    }
                    /*if (stanceNumber == 2)
                    {
                    }*/
                    if (stanceNumber == 3)
                    {
                        InputLock(4);
                    }
                }
                if (spr_dir == 3)
                {
                    if (stanceNumber == 1)
                    {
                        if (input != 6)
                        {
                            Trigger();
                        }
                        input = 6;
                        playerAnim.SetInteger("input", input);
                        InputLock(0.25);
                    }
                //if (stanceNumber == 2) {}
                if (stanceNumber == 3)
                    {
                       InputLock(4);
                    }
                }
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

    public void LoadSettings() {
        SceneManager.LoadScene("SettingsScreen");
    }



    //used by attacks to freeze all inputs but attacks if this is useful
    public void InputLock(double waitTime)
    { 
        inputActive = false;
        wait = waitTime;
    }

    public void Trigger()
    {
        playerAnim.ResetTrigger("trigger");
        playerAnim.SetTrigger("trigger");
    }

    void updateAttackScriptByStance() {
        if (stanceNumber == 1 || stanceNumber == 3) {
            meleeScript.enabled = true;
            projectileScript.enabled = false;
        } else if (stanceNumber == 2) {
            meleeScript.enabled = false;
            projectileScript.enabled = true;
        }
    }

}

