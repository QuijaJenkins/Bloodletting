using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mrDrHandler : MonoBehaviour
{
    
    //Variables for switching attack scripts based on stance
    public EnemyBite_new MRDRmeleeScript;
    public EnemyMoveShoot MRDRprojectileScipt;
    public mrDrMovement MRDRmovementScript;
    public EnemyChasePlayer MRDRenemyChaseScript;

    private int stanceNumber;

    // private MrDr mrDr; 
    //uncomment


    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindWithTag ("GameHandler") != null) {
                //   mrDr = GameObject.FindWithTag ("enemyShooter").GetComponent<MrDr> ();
                //uncomment
              }
         
        stanceNumber = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
            updateMrDrcriptByStance();
        
    }


    //Manage attack scripts so that MrDr cycles between attacks. 
    void updateMrDrcriptByStance() {
        if (stanceNumber == 1 || stanceNumber == 3) {
            MRDRprojectileScipt.enabled = false;
            MRDRmovementScript.enabled = false;
            MRDRenemyChaseScript.enabled = true;
            MRDRmeleeScript.enabled = true;
            MRDRprojectileScipt.Shot();
        } else if (stanceNumber == 2) {
             MRDRprojectileScipt.enabled = true;
            MRDRmovementScript.enabled = false;
            MRDRenemyChaseScript.enabled = false;
        }
    }

    public void switchMRDRStance(){
        Debug.Log("Stance switching! CurrStance: " + stanceNumber); 
        if (stanceNumber <=2) {
            stanceNumber++;
        } else {
            stanceNumber = 1;
        }
    }
    
    //allow mrdr melee scripts to see which stance we're on to use the correct attack damage + radius numbers
    public int getMRDRStance(){
        return stanceNumber;
    }

}
