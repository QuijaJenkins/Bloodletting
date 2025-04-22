using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameHandler_UpgradeMenu : MonoBehaviour {

        public static bool GameUpgradeisPaused = false;
        public GameObject upgradeUI;
        private GameHandler gameHandler;


        void Awake(){
        }

        void Start(){
                gameHandler = GameObject.FindObjectOfType<GameHandler>();
                upgradeUI.SetActive(false);
                GameUpgradeisPaused = false;
    }

        // void Update(){
        //         if (gameHandler.xp >= 100){
        //                 if (GameUpgradeisPaused){
        //                         Resume();
        //                 }
        //                 else{
        //                         Pause();
        //                 }
        //         }
        // }

        public void Pause(){
              if (!GameUpgradeisPaused){
                upgradeUI.SetActive(true);
                Time.timeScale = 0f;
                GameUpgradeisPaused = true;}
             else{ Resume ();}
             //NOTE: This added conditional is for a pause button
        }

        public void Resume(){
                upgradeUI.SetActive(false);
                Time.timeScale = 1f;
                GameUpgradeisPaused = false;
        }
        
        public void OpenUpgradeMenu() {
            if (!GameUpgradeisPaused) {
                upgradeUI.SetActive(true);
                Time.timeScale = 0f;
                GameUpgradeisPaused = true;
                Debug.Log("in upgrade menu");
            }
}
}