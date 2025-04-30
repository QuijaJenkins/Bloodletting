// using System.Collections.Generic;
// using System.Collections;
// using UnityEngine;

// public class PlayerAttackMelee : MonoBehaviour{

//       //public Animator animator;
//       private GameHandler gameHandler;
//       public Transform attackPt;

//       //for changing hitpoint location based on which direction you're walking from
//       public Transform attackPtW;
//       public Transform attackPtD;
//       public Transform attackPtA;
//       public Transform attackPtS;

//       public float attackRange = 0.5f;
//       public float attackRate = 2f;
//       private float nextAttackTime = 0f;
//       public int attackDamage = 40;
//       public LayerMask enemyLayers;
//       // public int stanceNum;
//       public int damageTakenFromAttack;
//       private GameObject bloodVFX;
//       public GameObject bloodVFX1;
//       public GameObject bloodVFX2;


//       void Start(){
//             damageTakenFromAttack = 5;
           
//             //set attack point to the right by default
//             attackPt = attackPtA;
           
           
//            //animator = gameObject.GetComponentInChildren<Animator>();
//             gameHandler = GameObject.FindObjectOfType<GameHandler>();

//             BloodRoll();
            
//       }

//       void Update(){
            
            
//             //change attack point based on which direction you're walking 
//             if (Input.GetKeyDown(KeyCode.W)) { 
//                   Debug.Log("W attack pt");
//                   attackPt = attackPtW;
//             } else if (Input.GetKeyDown(KeyCode.D)) { 
//                   Debug.Log("D attack pt");
//                   attackPt = attackPtD;
//             } else if (Input.GetKeyDown(KeyCode.A)) { 
//                   Debug.Log("A attack pt");
//                   attackPt = attackPtA;
//             } else if (Input.GetKeyDown(KeyCode.S)) { 
//                   Debug.Log("S attack pt");
//                   attackPt = attackPtS;
//             }

//             if (gameHandler.stanceNumber == 1) {
//                   attackRange = 0.3f;
//                   damageTakenFromAttack = 5;
//                   attackDamage = 50;
//             } else if (gameHandler.stanceNumber == 3) {
//                   attackRange = 1f;
//                   damageTakenFromAttack = 10;
//                   attackDamage = 100;
//             }
            
//             if (Time.time >= nextAttackTime){
//                   if (Input.GetKeyDown(KeyCode.Space)){
//                   // if (Input.GetAxis("Attack") > 0){
//                         Attack();
//                         nextAttackTime = Time.time + 1f / attackRate;
//                   // }
//                   }
//             }
//       }

//       void Attack(){
//             Debug.Log("We hit spacebar to attack");
//             if (gameHandler != null) {
//                   gameHandler.changeHealth(-damageTakenFromAttack, true);
//             }
//             //animator.SetTrigger ("Melee");
//             Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPt.position, attackRange, enemyLayers);
           
//             if (hitEnemies.Length == 0){
//                   Debug.Log("No enemies hit.");
//             }

//             foreach(Collider2D enemy in hitEnemies){
//                   Debug.Log("We hit " + enemy.name);

//                   // enemy.takeDamage(attackDamage);
//                   // EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
//                   // if (enemy != null)
//                   // {
//                   //       enemy.Enemy_health_will.takeDamage(attackDamage);
//                   //       // enemyHealth.takeDamage(attackDamage); // ✅ uses your public attackDamage
//                   //       gameHandler.changeHealth(20, false);
//                   // }


//                   Enemy_health_will enemyScript = enemy.GetComponent<Enemy_health_will>();

//                   // ✅ If the enemy has that script, deal damage and spray blood
//                   if (enemyScript != null)
//                   {
//                         enemyScript.takeDamage(attackDamage * gameHandler.attackMultiplier);
//                         GameObject bloodFX = Instantiate(bloodVFX, enemy.transform.position, Quaternion.identity);
//                         StartCoroutine(DestroyVFX(bloodFX));
//                         BloodRoll();
//                 // gameHandler.changeHealth(20, false);
//             }

//                   // Destroy(enemy.gameObject);
//                   // enemy.GetComponent<EnemyMeleeDamage>().TakeDamage(attackDamage);
//             }
//       }

//       //NOTE: to help see the attack sphere in editor:
//       void OnDrawGizmosSelected(){
//             if (attackPt == null) {return;}
//             Gizmos.DrawWireSphere(attackPt.position, attackRange);
//       }

//       IEnumerator DestroyVFX(GameObject theEffect)
//       {
//           yield return new WaitForSeconds(0.2f);
//           Destroy(theEffect);
//       }

//       void BloodRoll()
//     {
//         if (Random.Range(0,2) == 0)
//         {
//             bloodVFX = bloodVFX1;
//         }
//         else
//         {
//             bloodVFX = bloodVFX2;
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackMelee : MonoBehaviour
{
    private GameHandler gameHandler;
    public Transform attackPt;

    // Attack points for different directions
    public Transform attackPtW;
    public Transform attackPtD;
    public Transform attackPtA;
    public Transform attackPtS;

    public float attackRange = 0.5f;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public int attackDamage = 40;
    public LayerMask enemyLayers;
    public int damageTakenFromAttack;
    private GameObject bloodVFX;
    public GameObject bloodVFX1;
    public GameObject bloodVFX2;

    public float knockbackForce = 8f; // New: knockback force when attacking enemy

    void Start()
    {
        damageTakenFromAttack = 5;

        // Set default attack point
        attackPt = attackPtA;

        gameHandler = GameObject.FindObjectOfType<GameHandler>();
        BloodRoll();
    }

    void Update()
    {
        // Change attack point based on movement input
        if (Input.GetKeyDown(KeyCode.W))
        {
            attackPt = attackPtW;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            attackPt = attackPtD;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            attackPt = attackPtA;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            attackPt = attackPtS;
        }

        // Stance modifications
        if (gameHandler.stanceNumber == 1)
        {
            attackRange = 1f;
            damageTakenFromAttack = 5;
            attackDamage = 50;
        }
        else if (gameHandler.stanceNumber == 3)
        {
            attackRange = 1f;
            damageTakenFromAttack = 10;
            attackDamage = 100;
        }

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

//     void Attack()
// {
//     Debug.Log("We hit spacebar to attack");

//     if (gameHandler != null)
//     {
//         gameHandler.changeHealth(-damageTakenFromAttack, true);
//     }

//     Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPt.position, attackRange, enemyLayers);

//     if (hitEnemies.Length == 0)
//     {
//         Debug.Log("No enemies hit.");
//     }

//     foreach (Collider2D enemy in hitEnemies)
//     {
//         Debug.Log("We hit " + enemy.name);

//         Enemy_health_will enemyScript = enemy.GetComponent<Enemy_health_will>();
//         Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();

//         if (enemyScript != null && enemyRb != null)
//         {
//             // 1. Knockback BEFORE damage
//             Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
//             enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

//             // 2. Deal damage
//             enemyScript.takeDamage((int)(attackDamage * gameHandler.attackMultiplier));

//             // 3. Blood VFX
//             GameObject bloodFX = Instantiate(bloodVFX, enemy.transform.position, Quaternion.identity);
//             StartCoroutine(DestroyVFX(bloodFX));
//             BloodRoll();
//         }
//     }
// }

// working attack below: 
// void Attack()
// {
//     Debug.Log("We hit spacebar to attack");

//     if (gameHandler != null)
//     {
//         gameHandler.changeHealth(-damageTakenFromAttack, true);
//     }

//     Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPt.position, attackRange, enemyLayers);

//     if (hitEnemies.Length == 0)
//     {
//         Debug.Log("No enemies hit.");
//     }

//     foreach (Collider2D enemy in hitEnemies)
//     {
//         Debug.Log("We hit " + enemy.name);

//         Enemy_health_will enemyScript = enemy.GetComponent<Enemy_health_will>();
//         Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();

//         if (enemyScript != null)
//         {
//             if (enemyRb != null)
//             {
//                 Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
//                 enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
//             }

//             enemyScript.takeDamage((int)(attackDamage * gameHandler.attackMultiplier));

//             GameObject bloodFX = Instantiate(bloodVFX, enemy.transform.position, Quaternion.identity);
//             StartCoroutine(DestroyVFX(bloodFX));
//             BloodRoll();
//         }
//     }
// }

void Attack()
{
    Debug.Log("We hit spacebar to attack");

    if (gameHandler != null)
    {
        gameHandler.changeHealth(-damageTakenFromAttack, true);
    }

    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPt.position, attackRange, enemyLayers);

    if (hitEnemies.Length == 0)
    {
        Debug.Log("No enemies hit.");
    }

    foreach (Collider2D enemy in hitEnemies)
    {
        Debug.Log("We hit " + enemy.name);

        Enemy_health_will enemyScript = enemy.GetComponent<Enemy_health_will>();
        Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();

        if (enemyScript != null)
        {
            // 1. Deal damage
            enemyScript.takeDamage((int)(attackDamage * gameHandler.attackMultiplier));

            // 2. Knockback if still alive
            if (enemyScript.currentHealth > 0)
            {
                EnemyChasePlayer chaseScript = enemy.GetComponent<EnemyChasePlayer>();
                if (chaseScript != null)
                {
                    Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
                    chaseScript.ApplyKnockback(knockbackDirection, 8f); // 8f = knockback strength
                }
            }

            // 3. Blood VFX
            GameObject bloodFX = Instantiate(bloodVFX, enemy.transform.position, Quaternion.identity);
            StartCoroutine(DestroyVFX(bloodFX));
            BloodRoll();

            // 4. Kill enemy if health <= 0 (your Enemy_health_will handles destruction separately)
        }
    }
}


    // void Attack()
    // {
    //     Debug.Log("We hit spacebar to attack");

    //     if (gameHandler != null)
    //     {
    //         gameHandler.changeHealth(-damageTakenFromAttack, true);
    //     }

    //     Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPt.position, attackRange, enemyLayers);

    //     if (hitEnemies.Length == 0)
    //     {
    //         Debug.Log("No enemies hit.");
    //     }

    //     foreach (Collider2D enemy in hitEnemies)
    //     {
    //         Debug.Log("We hit " + enemy.name);

    //         Enemy_health_will enemyScript = enemy.GetComponent<Enemy_health_will>();
    //         Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();

    //         if (enemyScript != null)
    //         {
    //             // 1. Deal damage
    //             enemyScript.takeDamage((int)(attackDamage * gameHandler.attackMultiplier));

    //             // 2. Knockback enemy
    //             if (enemyRb != null)
    //             {
    //                 Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
    //                 enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    //             }

    //             // 3. Blood VFX
    //             GameObject bloodFX = Instantiate(bloodVFX, enemy.transform.position, Quaternion.identity);
    //             StartCoroutine(DestroyVFX(bloodFX));
    //             BloodRoll();

    //             // 4. Kill enemy if health <= 0
    //             if (enemyScript.currentHealth <= 0)
    //             {
    //                 Destroy(enemy.gameObject);
    //             }
    //         }
    //     }
    // }

    // Help see the attack area in Scene view
    void OnDrawGizmosSelected()
    {
        if (attackPt == null) { return; }
        Gizmos.DrawWireSphere(attackPt.position, attackRange);
    }

    IEnumerator DestroyVFX(GameObject theEffect)
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(theEffect);
    }

    void BloodRoll()
    {
        if (Random.Range(0, 2) == 0)
        {
            bloodVFX = bloodVFX1;
        }
        else
        {
            bloodVFX = bloodVFX2;
        }
    }
}

// using UnityEngine;

// public class PlayerAttackMelee : MonoBehaviour
// {
//     public Transform attackPoint;
//     public float attackRange = 0.5f;
//     public LayerMask enemyLayers;
//     public int attackDamage = 50;
//     public float attackRate = 2f;
//     private float nextAttackTime = 0f;
//     public float knockbackForce = 10f;

//     void Update()
//     {
//         if (Time.time >= nextAttackTime)
//         {
//             if (Input.GetKeyDown(KeyCode.Space))
//             {
//                 Attack();
//                 nextAttackTime = Time.time + 1f / attackRate;
//             }
//         }
//     }

// void Attack()
// {
//     Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPt.position, attackRange, enemyLayers);

//     foreach (Collider2D enemy in hitEnemies)
//     {
//         Enemy_health_will enemyScript = enemy.GetComponent<Enemy_health_will>();

//         if (enemyScript != null)
//         {
//             enemyScript.takeDamage(attackDamage);
//         }
//     }
// }



// //     void Attack()
// //     {
// //         Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

// //         foreach (Collider2D enemyCollider in hitEnemies)
// //         {
// //             Enemy_health_will enemyScript = enemyCollider.GetComponent<Enemy_health_will>();

// //             if (enemyScript != null)
// //             {
// //                 int totalDamage = Mathf.RoundToInt(attackDamage);

// //                 bool enemyStillAlive = enemyScript.takeDamage(totalDamage);

// //                 if (enemyStillAlive)
// //                 {
// //                     Rigidbody2D enemyRb = enemyCollider.GetComponent<Rigidbody2D>();

// //                     if (enemyRb != null)
// //                     {
// //                         Vector2 knockbackDirection = (enemyCollider.transform.position - transform.position).normalized;
// //                         enemyRb.velocity = Vector2.zero;
// //                         enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
// //                     }
// //                 }
// //             }
// //         }
// //     }

//     void OnDrawGizmosSelected()
//     {
//         if (attackPoint == null) return;
//         Gizmos.color = Color.red;
//         Gizmos.DrawWireSphere(attackPoint.position, attackRange);
//     }
// }
