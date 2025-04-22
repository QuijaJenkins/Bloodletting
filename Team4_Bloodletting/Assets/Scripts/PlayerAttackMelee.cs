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
      private GameObject bloodVFX;
      public GameObject bloodVFX1;
      public GameObject bloodVFX2;


      void Start(){
            damageTakenFromAttack = 5;
           
            //set attack point to the right by default
            attackPt = attackPtA;
           
           
           //animator = gameObject.GetComponentInChildren<Animator>();
            gameHandler = GameObject.FindObjectOfType<GameHandler>();

            BloodRoll();
            
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
                  damageTakenFromAttack = 5;
                  attackDamage = 50;
            } else if (gameHandler.stanceNumber == 3) {
                  attackRange = 1f;
                  damageTakenFromAttack = 10;
                  attackDamage = 100;
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

                  // enemy.takeDamage(attackDamage);
                  // EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
                  // if (enemy != null)
                  // {
                  //       enemy.Enemy_health_will.takeDamage(attackDamage);
                  //       // enemyHealth.takeDamage(attackDamage); // ✅ uses your public attackDamage
                  //       gameHandler.changeHealth(20, false);
                  // }


                  Enemy_health_will enemyScript = enemy.GetComponent<Enemy_health_will>();

                  // ✅ If the enemy has that script, deal damage and spray blood
                  if (enemyScript != null)
                  {
                        enemyScript.takeDamage(attackDamage * gameHandler.attackMultiplier);
                        GameObject bloodFX = Instantiate(bloodVFX, enemy.transform.position, Quaternion.identity);
                        StartCoroutine(DestroyVFX(bloodFX));
                        BloodRoll();
                // gameHandler.changeHealth(20, false);
            }

                  // Destroy(enemy.gameObject);
                  // enemy.GetComponent<EnemyMeleeDamage>().TakeDamage(attackDamage);
            }
      }

      //NOTE: to help see the attack sphere in editor:
      void OnDrawGizmosSelected(){
            if (attackPt == null) {return;}
            Gizmos.DrawWireSphere(attackPt.position, attackRange);
      }

      IEnumerator DestroyVFX(GameObject theEffect)
      {
          yield return new WaitForSeconds(0.2f);
          Destroy(theEffect);
      }

      void BloodRoll()
    {
        if (Random.Range(0,2) == 0)
        {
            bloodVFX = bloodVFX1;
        }
        else
        {
            bloodVFX = bloodVFX2;
        }
    }
}