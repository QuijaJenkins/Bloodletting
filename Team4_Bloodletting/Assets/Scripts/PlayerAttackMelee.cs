using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerAttackMelee : MonoBehaviour{

      //public Animator animator;
      private GameHandler gameHandler;
      public Transform attackPt;

      //for changing hitpoint location based on which direction you're walking from
      public Transform attackPtW;
      public Transform attackPtD;
      public Transform attackPtA;
      public Transform attackPtS;

      public float attackRange = 0.5f;
      public float attackRate = 2f;
      private float nextAttackTime = 0f;
      public int attackDamage = 40;
      public LayerMask enemyLayers;
      // public int stanceNum;
      public int damageTakenFromAttack;



      void Start(){
            damageTakenFromAttack = 10;
           
            //set attack point to the right by default
            attackPt = attackPtA;
           
           
           //animator = gameObject.GetComponentInChildren<Animator>();
            gameHandler = GameObject.FindObjectOfType<GameHandler>();

      }

      void Update(){
            
            
            //change attack point based on which direction you're walking 
            if (Input.GetKeyDown(KeyCode.W)) { 
                  Debug.Log("W attack pt");
                  attackPt = attackPtW;
            } else if (Input.GetKeyDown(KeyCode.D)) { 
                  Debug.Log("D attack pt");
                  attackPt = attackPtD;
            } else if (Input.GetKeyDown(KeyCode.A)) { 
                  Debug.Log("A attack pt");
                  attackPt = attackPtA;
            } else if (Input.GetKeyDown(KeyCode.S)) { 
                  Debug.Log("S attack pt");
                  attackPt = attackPtS;
            }

            if (gameHandler.stanceNumber == 1) {
                  attackRange = 0.3f;
                  damageTakenFromAttack = 10;
            } else if (gameHandler.stanceNumber == 3) {
                  attackRange = 1f;
                  damageTakenFromAttack = 20;
            }
            
            if (Time.time >= nextAttackTime){
                  if (Input.GetKeyDown(KeyCode.Space)){
                  // if (Input.GetAxis("Attack") > 0){
                        Attack();
                        nextAttackTime = Time.time + 1f / attackRate;
                  // }
                  }
            }
      }

      void Attack(){
            Debug.Log("We hit spacebar to attack");
            if (gameHandler != null) {
                  gameHandler.changeHealth(-damageTakenFromAttack, true);
            }
            //animator.SetTrigger ("Melee");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPt.position, attackRange, enemyLayers);
           
            if (hitEnemies.Length == 0){
                  Debug.Log("No enemies hit.");
            }

            foreach(Collider2D enemy in hitEnemies){
                  Debug.Log("We hit " + enemy.name);
                  Destroy(enemy.gameObject);
                  gameHandler.changeHealth(20, false);
                  // enemy.GetComponent<EnemyMeleeDamage>().TakeDamage(attackDamage);
            }
      }

      //NOTE: to help see the attack sphere in editor:
      void OnDrawGizmosSelected(){
            if (attackPt == null) {return;}
            Gizmos.DrawWireSphere(attackPt.position, attackRange);
      }
}