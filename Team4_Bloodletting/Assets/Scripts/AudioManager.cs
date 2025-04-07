
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

        public AudioSource MenuMusic;
        // public AudioSource GameMusic1;
        // public AudioSource GameMusic2;

        private AudioSource theMusic;
        public static float musicTimeStamp = 0.0f;
        public float currentTimeStamp;

        void Awake(){
                //set the music based on the scene
                if ((SceneManager.GetActiveScene().name == "MainMenu")
                || (SceneManager.GetActiveScene().name == "CreditScene")
                || (SceneManager.GetActiveScene().name == "SettingsScreen")
                //|| (SceneManager.GetActiveScene().name == "EndWin")
                ){
                        theMusic = MenuMusic;
                // } else {
                //         theMusic = GameMusic1;
                }

                //set the time and play:
                theMusic.time = musicTimeStamp;
                theMusic.Play();
        }

        void Update(){
               //keep track of timestamp, to auto-call it in the next scene:
               musicTimeStamp = theMusic.time;
               currentTimeStamp = theMusic.time;
        }

//change timestamp (can be called by door code):
        public void SetTimeStamp(){
               musicTimeStamp = theMusic.time;
        }
}