using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyBite_new: MonoBehaviour {
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
       public float timeToNextAttack = 2f;
       public bool canAttack = true;

        public Transform frontAttackPoint;
        public Transform backAttackPoint;
        public Transform leftAttackPoint;
        public Transform rightAttackPoint;


       void Start(){
              gameHandler = GameObject.FindWithTag ("GameHandler").GetComponent<GameHandler>();
              //anim = GetComponentInChildren<Animator>();
              player = GameObject.FindWithTag("Player").transform;
              lastPosition = transform.position;

       }

       void Update(){
            if (mrDrHandler.getMRDRStance() == 1) {
                attackRange = attackRange1;
                damageRange = damageRange1;
                damage = 10;
            } else if (mrDrHandler.getMRDRStance() == 3) {
                attackRange = attackRange3;
                damageRange = damageRange3;
                damage = 20;
            }

            movementDirection = (transform.position - lastPosition).normalized;
            lastPosition = transform.position;
             distanceToMouth = Vector3.Distance(player.position, GetDirectionalAttackPoint().position);
            if ((distanceToMouth < attackRange) && canAttack){
                Attack();
                StartCoroutine(AttackDelay());
            }


       }

       void Attack(){
            Transform attackPoint = GetDirectionalAttackPoint();
            float dist = Vector3.Distance(player.position, attackPoint.position);
            Debug.Log("I am biting the player!");

            if (dist < damageRange){
            gameHandler.changeHealth(-damage, false);
            }
            mrDrHandler.switchMRDRStance();
        }

       IEnumerator AttackDelay(){
              canAttack = false;
              yield return new WaitForSeconds(timeToNextAttack);
              canAttack = true;
       }

       //NOTE: to help see the attack sphere in editor:
       void OnDrawGizmos(){
            //   Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
            //   Gizmos.DrawWireSphere(AttackPoint.position, damageRange);
       }

    Transform GetDirectionalAttackPoint(){
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
}