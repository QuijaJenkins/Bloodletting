// using System.Collections.Generic;
// using System.Collections;
// using UnityEngine;
// using UnityEngine.UI;

// public class GameTimer : MonoBehaviour {
//     public float timeRemaining = 60f; // 1 minute in seconds
//     public Text timerText; 

//     private bool timerRunning = true;

//     void Update()
//     {
//         if (timerRunning)
//         {
//             if (timeRemaining > 0)
//             {
//                 timeRemaining -= Time.deltaTime;
//                 UpdateTimerDisplay();
//             }
//             else
//             {
//                 timeRemaining = 0;
//                 timerRunning = false;
//                 UpdateTimerDisplay();
//                 // Optional: Do something when time is up
//                 Debug.Log("Time's up!");
//             }
//         }
//     }

//     void UpdateTimerDisplay()
//     {
//         int minutes = Mathf.FloorToInt(timeRemaining / 60);
//         int seconds = Mathf.FloorToInt(timeRemaining % 60);
//         timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
//     }
// }