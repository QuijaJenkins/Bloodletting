using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HoldenTempScript : MonoBehaviour
{
    private Animator mAnim;
    private bool inputLocked = false;
    private double timer = 0;
    private double maxTime = 0;
    private int input;
    private bool attack1 = false;
    private bool attack2 = false;
    private bool attack3 = false;
    private int aTime;

    // Start is called before the first frame update
    void Start()
    {
        mAnim = gameObject.GetComponent<Animator>();
        InputLock(800);
        input = 0;
    }

    // Update is called once per frame
    void Update()
    {
        aTime++;
        if(aTime > 100 && aTime < 110)
        {
            attack2 = false;
            attack3 = false;
            attack1 = true;
        }
        else
        {
            attack1 = false;
        }
        if (aTime > 1000 && aTime < 1010)
        {
            attack1 = false;
            attack3 = false;
            attack2 = true;
        }
        else
        {
            attack2 = false;
        }
        if (aTime > 2000 && aTime < 2010)
        {
            attack1 = false;
            attack2 = false;
            attack3 = true;
        }
        else
        {
            attack3 = false;
        }
        if (aTime > 3000)
        {
            aTime = 0;
        }

        //timer function
        if (inputLocked)
        {
            if (timer >= maxTime)
            {
                inputLocked = false;
                timer = 0;
            }
            else
            {
                timer++;
            }
        }

        //attack animation code
        if (!inputLocked)
        {
            if (attack1)
            {
                if (input != 1)
                {
                    mAnim.SetInteger("input", 1);
                    Trigger();
                    InputLock(720);
                }
                input = 1;
            } else if (attack2)
            {
                if (input != 2)
                {
                    mAnim.SetInteger("input", 2);
                    Trigger();
                    InputLock(740);
                }
                input = 2;
                
            }
            else if (attack3)
            {
                if (input != 3)
                {
                    mAnim.SetInteger("input", 3);
                    Trigger();
                    InputLock(750);
                }
                input = 3;
                
            }
            else
            {
                if (input != 0)
                {
                    mAnim.SetInteger("input", 0);
                    Trigger();
                }
                input = 0;
            }

        }
    }

    private void InputLock(double time)
    {
        maxTime = time;
        inputLocked = true;
    }

    private void Trigger()
    {
        mAnim.ResetTrigger("trigger");
        mAnim.SetTrigger("trigger");
    }
}
