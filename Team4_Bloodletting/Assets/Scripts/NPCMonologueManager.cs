using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NPCMonologueManager : MonoBehaviour {

       public GameObject monologueBox;
       public Text monologueText;
       public string[] monologue;
       public int counter = 0;
       public int monologueLength;

       void Start(){
              monologueBox.SetActive(false);
              monologueLength = monologue.Length; //allows us test dialogue without an NPC
       }

       void Update(){
              //temporary testing before NPC is created
              if (Input.GetKeyDown("o")){
                     monologueBox.SetActive(true);
              }
              if (Input.GetKeyDown("p")){
                     monologueBox.SetActive(false);
                     monologueText.text = "..."; //reset text
                     counter = 0; //reset counter
              }
       }

       public void OpenMonologue(){
              monologueBox.SetActive(true);
 
              //auto-loads the first line of monologue
              monologueText.text = monologue[0];
              counter = 1;
       }

       public void CloseMonologue(){
              monologueBox.SetActive(false);
              monologueText.text = "..."; //reset text
              counter = 0; //reset counter
       }

       public void LoadMonologueArray(string[] NPCscript, int scriptLength){
              monologue = NPCscript;
              monologueLength = scriptLength;
       }

        //function for the button to display next line of dialogue
       public void MonologueNext(){
              if (counter < monologueLength){
                     monologueText.text = monologue[counter];
                     counter +=1;
              }
              else { //when lines are complete:
                     monologueBox.SetActive(false); //turn off the dialogue display
                     monologueText.text = "..."; //reset text
                     counter = 0; //reset counter
              }
       }

}