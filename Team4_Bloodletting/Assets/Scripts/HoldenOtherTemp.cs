using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public class HoldenOtherTemp : MonoBehaviour
{
    private bool trigger = false;
    private GameObject player;
    public GameObject movePoint;
    public GameObject crystal;
    public GameObject desk;
    public GameObject art;
    public GameObject finalArt;
    private Animator crystalAnim;
    private Animator deskAnim;
    private double timer = 0;
    private double maxTime = 550;
    private double freezeTime = 290;
    private double shatterTime = 77;
    private bool moved;

    public Image image;
    public CanvasGroup canvasGroup;
    private bool fadeIn = false;
    private bool fadeOut = false;
    private float fadeRate = 2;


    // Start is called before the first frame update
    void Start()
    {
        crystalAnim = crystal.GetComponent<Animator>();
        deskAnim = desk.GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        trigger = true;
    }


    void Update()
    {
        //screen transition code
        if (fadeIn)
        {
            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += fadeRate * Time.deltaTime;
                if (canvasGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }
        if (fadeOut)
        {
            if (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= fadeRate * Time.deltaTime;
                if (canvasGroup.alpha <= 0)
                {
                    fadeOut = false;
                }
            }
        }

        if (trigger == true)
        {
            timer++;

            //set animations for crystal and desk
            deskAnim.SetTrigger("trigger");
            if (timer >= shatterTime)
            {
                crystalAnim.enabled = true;
            }

            if(timer >= freezeTime && timer < maxTime)
            {
                FadeIn();
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

    public void FadeIn()
    {
        fadeIn = true;
    }
    public void FadeOut()
    {
        fadeOut = true;
        Destroy(crystal);
        Destroy(desk);
        Destroy(art);
        finalArt.SetActive(true);
    }

}
