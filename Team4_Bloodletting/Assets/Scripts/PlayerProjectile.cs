using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour{

      private GameHandler gameHandler;


      public int arrowAttackDamage = 99;
      public GameObject hitEffectAnim;
      public float SelfDestructTime = 4.0f;
      public float SelfDestructVFX = 0.5f;
      public GameObject projectileArt;
      private GameObject bloodVFX;
      public GameObject bloodVFX1;
      public GameObject bloodVFX2;
    //public GameObject splatterPrefab;

    void Start(){
           //projectileArt = GetComponentInChildren<SpriteRenderer>();
            StartCoroutine(selfDestruct());
            gameHandler = GameObject.FindObjectOfType<GameHandler>();

            BloodRoll();
    }

      //if the bullet hits a collider, play the explosion animation, then destroy the effect and the bullet
      void OnTriggerEnter2D(Collider2D other){
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemies")) {
                  //gameHandlerObj.playerGetHit(damage);

                  Debug.Log("We hit an enemy with the arrow");
                  Debug.Log("arrow attack damage: " + arrowAttackDamage);



                  // Destroy(other.gameObject);

                  // gameHandler.changeHealth(20, false);

                  Enemy_health_will enemyScript = other.GetComponent<Enemy_health_will>();

                  // ✅ If the enemy has that script, deal damage
                  if (enemyScript != null)
                  {
                        enemyScript.takeDamage(arrowAttackDamage * gameHandler.attackMultiplier);
                        // gameHandler.changeHealth(20, false);
                        GameObject bloodFX = Instantiate(bloodVFX, other.transform.position, Quaternion.identity);
                        StartCoroutine(DestroyVFX(bloodFX));
                        BloodRoll();
            }
                  
                  //We do not have enemyMeleeDamage so i am commenting it out
                //   other.gameObject.GetComponent<EnemyMeleeDamage>().TakeDamage(damage);
            } else {
                  Debug.Log("We hit layer: " + other.gameObject.layer + " Game object named: " + other);
            }   
           if (other.gameObject.tag != "Player") {
                  gameObject.GetComponent<BoxCollider2D>().enabled = false;
                  GameObject animEffect = Instantiate (hitEffectAnim, transform.position, Quaternion.identity);
                  projectileArt.SetActive(false);
                  //Destroy (animEffect, 0.5);
                    StartCoroutine(SelfDestructHit(animEffect));
            }
      }

      void FixedUpdate(){
      //   Debug.Log("Position of Arrow: "+ transform.position);
      }

      IEnumerator SelfDestructHit(GameObject VFX){
            //MakeSplat();
            yield return new WaitForSeconds(SelfDestructVFX);
            Destroy (VFX);
            Destroy (gameObject);
      }

      IEnumerator selfDestruct(){
            yield return new WaitForSeconds(SelfDestructTime);
            Destroy (gameObject);
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
    /*
    //Make a mark on the ground where the projectile hit
    void MakeSplat(){
          GameObject splat = Instantiate (splatterPrefab, transform.position, Quaternion.identity);
          float zRotation = Random.Range(0f,179f);
          splat.transform.eulerAngles = new Vector3(0, 0, zRotation);
          float size = Random.Range(0.5f,0.9f);
          splat.transform.localScale = new Vector3(splat.transform.localScale.x * size, splat.transform.localScale.y * size, 1);
    }
    */
}