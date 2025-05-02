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