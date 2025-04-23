using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StanceIndicator : MonoBehaviour
{
    public GameHandler gamescript;
    public int stanceNumber;
    public GameObject Stance1;
    public GameObject Stance2;
    public GameObject Stance3;
    public GameObject Stance1Locked;
    public GameObject Stance2Locked;
    public GameObject Stance3Locked;

    public string sceneName;
    
    public bool s1locked;
    public bool s2locked;
    public bool s3locked;



    // Start is called before the first frame update
    void Start()
    {
        // deselect all stances
        Stance1.SetActive(false);
        Stance2.SetActive(false);
        Stance3.SetActive(false);

        // get current scene and lock appropriate stances
        sceneName = gamescript.sceneName;
    }

    // Update is called once per frame
    void Update()
    {
        // fetch stanceNumber & current scene from the GameHandler
        stanceNumber = gamescript.stanceNumber;
        sceneName = gamescript.sceneName;
        s1locked = gamescript.s1locked;
        s2locked = gamescript.s2locked;
        s3locked = gamescript.s3locked;
        // if (sceneName == "Level1") {
        //     s2locked = true;
        //     s3locked = true;
        // }
        // if (sceneName == "Level 2") {
        //     s3locked = true;
        // }

        // if (sceneName == "Level 3") {
        //     s1locked = false;
        //     s2locked = false;
        //     s3locked = false;
        // }

        // TO DO!!!!!!
        // fetch curr wave of level, unlock stances as needed
        // (ex: s1locked = false once level2 finishes wave 2)

        
        // update locked stances
        if (s1locked) {
            Stance1Locked.SetActive(true);
        } 
        else {
            Stance1Locked.SetActive(false);
        }
        if (s2locked) {
            Stance2Locked.SetActive(true);
        } 
        else {
            Stance2Locked.SetActive(false);
        }
        if (s3locked) {
            Stance3Locked.SetActive(true);
        } 
        else {
            Stance3Locked.SetActive(false);
        }


        // update stance based on input
        if (stanceNumber == 1 && !s1locked) {
            Stance1.SetActive(true);
            Stance2.SetActive(false);
            Stance3.SetActive(false);
        }
        else if (stanceNumber == 2 && !s2locked) {
            Stance1.SetActive(false);
            Stance2.SetActive(true);
            Stance3.SetActive(false);
        } else if (stanceNumber == 3 && !s3locked) {
            Stance1.SetActive(false);
            Stance2.SetActive(false);
            Stance3.SetActive(true);
        }

    }
}
