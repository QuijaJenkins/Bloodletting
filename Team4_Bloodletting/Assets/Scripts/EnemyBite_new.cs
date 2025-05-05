using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class EnemyBite_new : MonoBehaviour
{
    // private Animator anim;
    private GameHandler gameHandler;
    public mrDrHandler mrDrHandler;


    private Transform player;
    public Transform AttackPoint;
    public float attackRange = 3f;
    public float damageRange = 5f;
    private float attackRange1 = 2.1f;
    private float damageRange1 = 2.2f;
    private float attackRange3 = 3f;
    private float damageRange3 = 3.1f;
    //public LayerMask playerLayer;

    private Vector3 lastPosition;
    private Vector3 movementDirection;

    public int damage = 10;
    private float distanceToMouth;
    public float timeToNextAttack = 200f;
    public bool canAttack = true;

    public Transform frontAttackPoint;
    public Transform backAttackPoint;
    public Transform leftAttackPoint;
    public Transform rightAttackPoint;

    private float aDelayMaxTime;
    private float aDelayTime;
    private bool aReady;
    private bool attacking;

    private Animator mAnim;
    private bool inputLocked = false;
    private double timer = 0;
    private double maxTime = 0;
    private int input;
    private bool attack1 = false;
    private bool attack2 = false;
    private bool attack3 = false;
    private int aTime;

    void Start()
    {
        gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        //anim = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        lastPosition = transform.position;
        mAnim = gameObject.GetComponent<Animator>();
        InputLock(800);
        input = 0;
    }

    void Update()
    {
        if (mrDrHandler.getMRDRStance() == 1)
        {
            attackRange = attackRange1;
            damageRange = damageRange1;
            damage = 10;
        }
        else if (mrDrHandler.getMRDRStance() == 3)
        {
            attackRange = attackRange3;
            damageRange = damageRange3;
            damage = 20;
        }

        movementDirection = (transform.position - lastPosition).normalized;
        lastPosition = transform.position;
        distanceToMouth = Vector3.Distance(player.position, GetDirectionalAttackPoint().position);
        if ((distanceToMouth < attackRange) && canAttack)
        {
            Attack();
            StartCoroutine(AttackDelay());
        }

        //attack delay
        if (!aReady)
        {
            if (aDelayTime >= aDelayMaxTime)
            {
                attacking = true;
                aDelayTime = 0;
                aReady = true;
            }
            else
            {
                aDelayTime++;
            }
        }
        //actual attack
        if (attacking)
        {
            Transform attackPoint = GetDirectionalAttackPoint();
            float dist = Vector3.Distance(player.position, attackPoint.position);
            Debug.Log("I am biting the player!");

            if (dist < damageRange)
            {
                gameHandler.changeHealth(-damage, false);
            }
            mrDrHandler.switchMRDRStance();
            attacking = false;
            attack1 = false;
            attack2 = false;
            attack3 = false;
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
            }
            else if (attack2)
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

    public void Attack()
    {
        if (mrDrHandler.getMRDRStance() == 1)
        {
            aReady = false;
            aDelayMaxTime = 300;
            attack1 = true;
        }
        if (mrDrHandler.getMRDRStance() == 3)
        {
            aReady = false;
            aDelayMaxTime = 400;
            attack3 = true;
        }
    }

    IEnumerator AttackDelay()
    {
        canAttack = false;
        yield return new WaitForSeconds(timeToNextAttack);
        canAttack = true;
    }

    //NOTE: to help see the attack sphere in editor:
    void OnDrawGizmos()
    {
        //   Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
        //   Gizmos.DrawWireSphere(AttackPoint.position, damageRange);
    }

    Transform GetDirectionalAttackPoint()
    {
        Vector2 toPlayer = (player.position - transform.position).normalized;

        float angle = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg;

        // Normalize angle to [-180, 180], then use quadrant ranges
        if (angle >= -45f && angle <= 45f)
            return rightAttackPoint;  // Player is to the right
        else if (angle > 45f && angle <= 135f)
            return frontAttackPoint;  // Player is above (up)
        else if (angle < -45f && angle >= -135f)
            return backAttackPoint;   // Player is below (down)
        else
            return leftAttackPoint;   // Player is to the left
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