using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NPCMonologue : MonoBehaviour {
       //private Animator anim;
       private NPCMonologueManager monologueMNGR;
       public string[] monologue; //enter monologue lines into the inspector for each NPC
       public bool playerInRange = false; //could be used to display an image: hit [e] to talk
       public int monologueLength;

       void Start(){
              //anim = gameObject.GetComponentInChildren<Animator>();
              monologueLength = monologue.Length;
              if (GameObject.FindWithTag("MonologueManager")!= null){
                     monologueMNGR = GameObject.FindWithTag("MonologueManager").GetComponent<NPCMonologueManager>();
              }
       }

       private void OnTriggerEnter2D(Collider2D other){
              if (other.gameObject.tag == "Player") {
                     playerInRange = true;
                     monologueMNGR.LoadMonologueArray(monologue, monologueLength);
                     monologueMNGR.OpenMonologue();
                     //anim.SetBool("Chat", true);
                     //Debug.Log("Player in range");
              }
       }

       private void OnTriggerExit2D(Collider2D other){
              if (other.gameObject.tag =="Player") {
                     playerInRange = false;
                     monologueMNGR.CloseMonologue();
                     //anim.SetBool("Chat", false);
                     //Debug.Log("Player left range");
              }
       }
}


// using System.Collections.Generic;
// using System.Collections;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro; // Add this
// using UnityEngine;

// public class NPCMonologue : MonoBehaviour {
//     public string[] monologue;
//     public bool playerInRange = false;

//     public TextMeshPro monologueText; // Assign this in the Inspector
//     private int currentLine = 0;
//     private Coroutine typingRoutine;

//     void Start() {
//         monologueText.text = "";
//     }

//     private void OnTriggerEnter2D(Collider2D other) {
//         if (other.CompareTag("Player")) {
//             playerInRange = true;
//             currentLine = 0;
//             typingRoutine = StartCoroutine(ShowMonologue());
//         }
//     }

//     private void OnTriggerExit2D(Collider2D other) {
//         if (other.CompareTag("Player")) {
//             playerInRange = false;
//             StopCoroutine(typingRoutine);
//             monologueText.text = "";
//         }
//     }

//     IEnumerator ShowMonologue() {
//         while (currentLine < monologue.Length) {
//             monologueText.text = monologue[currentLine];
//             currentLine++;
//             yield return new WaitForSeconds(3f); // Adjust time per line
//         }
//         monologueText.text = "";
//     }
// }
