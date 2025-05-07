using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public class HoldenOtherTemp : MonoBehaviour
{
    private bool trigger = true;
    private GameObject player;
    public GameObject movePoint;
    public GameObject crystal;
    public GameObject desk;
    public GameObject art;
    public GameObject finalArt;
    public GameObject mrDr;
    private Animator crystalAnim;
    private Animator deskAnim;
    private double timer = 0;
    private double maxTime = 550;
    private double freezeTime = 290;
    private double shatterTime = 77;
    private bool moved;



    // Start is called before the first frame update
    void Start()
    {
        crystalAnim = crystal.GetComponent<Animator>();
        deskAnim = desk.GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
    }


    void Update()
    {

        if (trigger == true)
        {
            timer++;

            //set animations for crystal and desk
            if (desk) {
                deskAnim.SetTrigger("trigger");
            }
            
            if (timer >= shatterTime)
            {
                if (crystal) {
                    crystalAnim.enabled = true;
                }
                
            }

            if (timer >= maxTime)
            {
                if (!moved)
                {
                    player.transform.position = movePoint.transform.position;
                    moved = true;
                }
                FadeOut();
                trigger = false;
            }            
        }
    }


    public void FadeOut()
    {
        Destroy(crystal);
        Destroy(desk);
        Destroy(art);
        finalArt.SetActive(true);
        //GameObject boss = Instantiate(mrDr,transform.position, Quaternion.identity);
    }

}
