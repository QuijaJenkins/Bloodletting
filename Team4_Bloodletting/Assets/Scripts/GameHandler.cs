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
    public int maxPlayerHealth = 100;
    public UnityEngine.UI.Image healthBar;
    public UnityEngine.UI.Image XPBar;
    public float moveSpeed = 5f;


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

    public int xp = 0;

    //Variables for switching attack scripts based on stance
    public PlayerAttackMelee meleeScript;
    public PlayerMoveAimShoot projectileScript;
    public playerMove playerMoveScript;
    private GameHandler_UpgradeMenu upgradeMenuScript;

    // vars for stance indicator & locking based on lvl
    public bool s1locked = false;
    public bool s2locked = true;
    public bool s3locked = true;

    //starting values for upgrade multipliers
    private bool isChoosingUpgrade = false;
    public float attackMultiplier = 1;
    public float speedMultiplier = 1;
    public float healthMultiplier = 1;
    static public bool hard = true; // this might cause issues if another script doesn't set the var before WaveManager runs

    // audio vars
    public AudioSource playerhurt_sfx;

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
    public int stanceSwap3 = 14;
    public int hurt = 15;
    */
    
    // public GameObject healthText;

    // maybe should be private?? but i'm chainging to public for Stance Indicator
    public string sceneName;

    // var to start waves after tutorial
    public bool dialogue_complete = false;
    public GameObject Lvl1_HUD;
    public GameObject NPC; 
    public GameObject skiptut_text;
    
    // Start is called before the first frame update
    void Start(){
        //start in first stance
        stanceNumber = 1;
        updateAttackScriptByStance();
        upgradeMenuScript = GameObject.FindObjectOfType<GameHandler_UpgradeMenu>();


        player = GameObject.FindWithTag("Player");
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName=="MainMenu"){ //uncomment these two lines when the MainMenu exists
            playerHealth = StartPlayerHealth;
        }
        playerAnim = player.GetComponentInChildren<Animator>();
        updateStatsDisplay();
        spr_dir = 2;

        // for level 1 HUD
        if (Lvl1_HUD != null){
            Lvl1_HUD.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // TESTING for upgradeMenu
        // if (Input.GetKeyDown(KeyCode.X)) { 
        //     xp += 50;    
        // }

        // NPC HANDLING
        // shows "skip dialogue" text
        if (skiptut_text != null){ 
            if(!dialogue_complete){
                skiptut_text.SetActive(true);
            }
        } 

        // checks for tutorial over to start waves
        if (Input.GetKeyDown(KeyCode.T) && !dialogue_complete && skiptut_text != null) {
            if (skiptut_text != null){ 
                skiptut_text.SetActive(false);
            }

            dialogue_complete = true;
            NPC.SetActive(false);
            Debug.Log("TUTORIAL COMPLETE");

            // reset their health for whatever they lost in tutorial
            playerHealth = maxPlayerHealth;

            // show lvl 1 HUD for 30s
            if (sceneName == "Level1") {
                Lvl1_HUD.SetActive(true);
                StartCoroutine(HideHUDDelayed(30f));
            }
        }

        

        //trigger player upgrade if their xp hits 100
        if (!isChoosingUpgrade && xp >= 100) {
            isChoosingUpgrade = true;
            upgradeMenuScript.OpenUpgradeMenu();
            Debug.Log("Choose an upgrade: [5] Health, [6] Attack, [7] Speed");
        }

        // if (isChoosingUpgrade) {
        //     if (Input.GetKeyDown(KeyCode.Alpha5)) {
        //         healthMultiplier += 0.2f;
        //         int healthGain = Mathf.RoundToInt(maxPlayerHealth * 0.2f);
        //         maxPlayerHealth += healthGain;
        //         playerHealth += healthGain;
        //         finishUpgrade();
        //     } 
        //     //attack upgrade
        //     else if (Input.GetKeyDown(KeyCode.Alpha6)) {
        //         attackMultiplier += 0.2f;
        //         finishUpgrade();
        //     } else if (Input.GetKeyDown(KeyCode.Alpha7)) {
        //         speedMultiplier += 0.2f;
        //         // movement.UpdateMoveSpeed();
        //         playerMoveScript.UpdateMoveSpeed();            
        //         finishUpgrade();
        //     }
        // }
        
        if (healthBar != null) {
            healthBar.fillAmount = playerHealth / (float)maxPlayerHealth;
        }
        if (XPBar != null) {
            XPBar.fillAmount = xp / 100f;
        }


        if (playerHealth <= 0) {
            DeathScreen();
        }

        // s1locked = stance_script.s1locked;
        // s2locked = stance_script.s2locked;
        // s3locked = stance_script.s3locked;

        // tutorial & lvl1 get knife
        if (sceneName == "Level1" || sceneName == "Level 2") {
            s1locked = false;
            s2locked = true;
            s3locked = true;
        }
        // level 3 gets bow
        if (sceneName == "Level 3") {
            s1locked = false;
            s2locked = false;
            s3locked = true;
        }
        // level 3 gets axe
        if (sceneName == "Level 4") {
            s1locked = false;
            s2locked = false;
            s3locked = false;
        }

        //code for stance switching. Click 1 2 or 3 for respective stance
        if (Input.GetKeyDown(KeyCode.Alpha1) && stanceNumber != 1 && !s1locked) { 
            Debug.Log("Entering stance 1");
            stanceNumber = 1;
            updateAttackScriptByStance();
        } else if (Input.GetKeyDown(KeyCode.Alpha2) && stanceNumber != 2 && !s2locked) {
            Debug.Log("Entering stance 2");
            stanceNumber = 2;
            updateAttackScriptByStance();
        } else if (Input.GetKeyDown(KeyCode.Alpha3) && stanceNumber != 3 && !s3locked) {
            Debug.Log("Entering stance 3");
            stanceNumber = 3;
            updateAttackScriptByStance();
        }

        movement = player.GetComponent<playerMove>().movement;
        
        //stance
        playerAnim.SetInteger("stance", stanceNumber);
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

        //current state code
        if (spr_dir == 4)
        {
            player.GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        else if (spr_dir == 2)
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

            if (movement.y > Mathf.Abs(movement.x) || (Mathf.Abs(movement.x) == Mathf.Abs(movement.y) && movement.y > 0))
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

            if (movement.y < -Mathf.Abs(movement.x) || (Mathf.Abs(movement.x) == Mathf.Abs(movement.y) && movement.y < 0))
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

            /*if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (input != 0)
                {
                    Trigger();
                }
                input = 5;
                playerAnim.SetInteger("input", input);
                InputLock(.4);
            }*/

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
                     if (stanceNumber == 2) 
                    {
                        if (input != 7)
                        {
                            Trigger();
                        }
                        input = 7;
                        playerAnim.SetInteger("input", input);
                        InputLock(0.25);
                    }

                     if (stanceNumber == 3)
                     {
                        if (input != 8)
                        {
                            Trigger();
                        }
                        input = 8;
                        playerAnim.SetInteger("input", input);
                        InputLock(.9);
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
                    if (stanceNumber == 2)
                    {
                        if (input != 7)
                        {
                            Trigger();
                        }
                        input = 7;
                        playerAnim.SetInteger("input", input);
                        InputLock(0.25);
                    }

                    if (stanceNumber == 3)
                    {
                        if (input != 8)
                        {
                            Trigger();
                        }
                        input = 8;
                        playerAnim.SetInteger("input", input);
                        InputLock(.9);
                    }
                }
                if(spr_dir == 1) 
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
                    if (stanceNumber == 2)
                    {
                        if (input != 7)
                        {
                            Trigger();
                        }
                        input = 7;
                        playerAnim.SetInteger("input", input);
                        InputLock(0.25);
                    }
                    if (stanceNumber == 3)
                    {
                        if (input != 8)
                        {
                            Trigger();
                        }
                        input = 8;
                        playerAnim.SetInteger("input", input);
                        InputLock(.9);
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
                    if (stanceNumber == 2)
                    {
                        if (input != 7)
                        {
                            Trigger();
                        }
                        input = 7;
                        playerAnim.SetInteger("input", input);
                        InputLock(0.25);
                    }
                    if (stanceNumber == 3)
                    {
                        if (input != 8)
                        {
                            Trigger();
                        }
                        input = 8;
                        playerAnim.SetInteger("input", input);
                        InputLock(.9);
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
        if (playerHealth + healthChange >= maxPlayerHealth) {
            playerHealth = maxPlayerHealth;
        }
        else{
            //player cannot kill themself by attack, leave them on 1hp
            if (playerAttack && playerHealth + healthChange <= 0) {
                playerHealth = 1;
            }
            else {
                playerHealth += healthChange;
                if (!playerAttack) {
                    playerhurt_sfx.Play();
                }
            }
        }
    }

    // public void playerHurtAudio() {
    //     // TESTING
    //     //playerhurt_sfx.Play();
    //     Debug.Log("Player hurt sound played.");
    // }


    // RESETS ALL STATIC VARIABLES FOR NEW GAMES
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

    // Doesn't reset variables for new game, should not be used after a run
    // only use between menus
    public void LoadMainMenu() {
            SceneManager.LoadScene("MainMenu");
    }

    public void Credits() {
        SceneManager.LoadScene("CreditScene");
    }

    public void LoadSettings() {
        SceneManager.LoadScene("SettingsScreen");
    }

    public void DeathScreen() {
        SceneManager.LoadScene("DeathScreen");
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

    //triggered when player reaches 
    void upgradeLevel() {

        // while (!(Input.GetKeyDown(KeyCode.Alpha5)) && !(Input.GetKeyDown(KeyCode.Alpha6)) && !(Input.GetKeyDown(KeyCode.Alpha7))){
            //Health Upgrade

            //New Dilemma: healthbar is not dynamically sizing, so players do not see
            // their health increase, just that it goes down less quickly in the healthbar. 
            // A Number for visualization woudl be useful. 
            if (Input.GetKeyDown(KeyCode.Alpha5)) {
                healthMultiplier += 0.2f;
                maxPlayerHealth = Mathf.RoundToInt(maxPlayerHealth * healthMultiplier);
                playerHealth = Mathf.RoundToInt(playerHealth * healthMultiplier);
            } 
            //attack upgrade
            else if (Input.GetKeyDown(KeyCode.Alpha6)) {
                attackMultiplier += 0.2f;
            } else if (Input.GetKeyDown(KeyCode.Alpha7)) {
                speedMultiplier += 0.2f;
                // playerMoveScript.UpdateMoveSpeed();            
            }
        
            xp -= 100;

        }
    // }

    void finishUpgrade(){
        xp -= 100;
        isChoosingUpgrade = false;
        upgradeMenuScript.Resume();
    }

    // move to next level 
    public void GoToNextLevel()
    {
    string current = SceneManager.GetActiveScene().name;

    if (current == "Level1")
        SceneManager.LoadScene("Level 2");
    else if (current == "Level 2")
        SceneManager.LoadScene("Level 3");
    else if (current == "Level 3")
        SceneManager.LoadScene("Level 4");
    else if (current == "Level 4")
       SceneManager.LoadScene("WinScreen");
    else
        Debug.Log("No next level defined for " + current);
    }


    IEnumerator HideHUDDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (Lvl1_HUD != null)
        {
            Lvl1_HUD.SetActive(false);
        }
    }





    public void healthUpgrade() {
        healthMultiplier += 0.2f;
        int healthGain = Mathf.RoundToInt(maxPlayerHealth * 0.2f);
        maxPlayerHealth += healthGain;
        playerHealth += healthGain;
        finishUpgrade();
    }
    public void attackUpgrade() {
        attackMultiplier += 0.2f;
        finishUpgrade();
    }
    public void speedUpgrade() {
        speedMultiplier += 0.2f;
        // movement.UpdateMoveSpeed();
        playerMoveScript.UpdateMoveSpeed();            
        finishUpgrade();
    }


}



