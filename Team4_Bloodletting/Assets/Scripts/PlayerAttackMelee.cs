using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerAttackMelee : MonoBehaviour{

      //public Animator animator;
      private GameHandler gameHandler;
      public Transform attackPt;
      public float attackRange = 0.5f;
      public float attackRate = 2f;
      private float nextAttackTime = 0f;
      public int attackDamage = 40;
      public LayerMask enemyLayers;

      void Start(){
           //animator = gameObject.GetComponentInChildren<Animator>();
            gameHandler = GameObject.FindObjectOfType<GameHandler>();

      }

      void Update(){
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
                  gameHandler.changeHealth(-10, true);
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