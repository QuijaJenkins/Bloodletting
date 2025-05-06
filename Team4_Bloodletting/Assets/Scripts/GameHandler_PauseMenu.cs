using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameHandler_PauseMenu : MonoBehaviour {

        public static bool GameisPaused = false;
        public GameObject pauseMenuUI;
        public AudioMixer music_mixer;
        public AudioMixer sfx_mixer;
        public static float musicVolumeLevel = 1.0f;
        public static float sfxVolumeLevel = 1.0f;
        private Slider music_sliderVolumeCtrl;
        private Slider sfx_sliderVolumeCtrl;

        void Awake(){
                pauseMenuUI.SetActive(true); // so slider can be set
                MusicSetLevel (musicVolumeLevel);
                SFXSetLevel (sfxVolumeLevel);
                GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
                if (sliderTemp != null){
                        music_sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
                        music_sliderVolumeCtrl.value = musicVolumeLevel;
                }

                // sfx
                GameObject sliderTemp1 = GameObject.FindWithTag("PauseMenuSlider_sfx");
                if (sliderTemp1 != null){
                        sfx_sliderVolumeCtrl = sliderTemp1.GetComponent<Slider>();
                        sfx_sliderVolumeCtrl.value = sfxVolumeLevel;
                }
        }

        void Start(){
                pauseMenuUI.SetActive(false);
                GameisPaused = false;
        }

        void Update(){
                if (Input.GetKeyDown(KeyCode.Escape)){
                        if (GameisPaused){
                                Resume();
                        }
                        else {
                                Pause();
                        }
                }
        }


        public void Pause(){
              if (!GameisPaused){
                pauseMenuUI.SetActive(true);
                Time.timeScale = 0f;
                GameisPaused = true;}
             else{ Resume ();}
             //NOTE: This added conditional is for a pause button
        }

        public void Resume(){
                pauseMenuUI.SetActive(false);
                Time.timeScale = 1f;
                GameisPaused = false;
        }

        public void MusicSetLevel(float sliderValue){
                music_mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
                musicVolumeLevel = sliderValue;
        }

        public void SFXSetLevel(float sliderValue){
                sfx_mixer.SetFloat("SFXVolume", Mathf.Log10 (sliderValue) * 20);
                sfxVolumeLevel = sliderValue;
        }
}