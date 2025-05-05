 using System.Collections.Generic;
 using System.Collections;
 using UnityEngine;

 public class playerMove : MonoBehaviour {

       public Rigidbody2D rb;
       public float moveSpeed = 5f;
       public Vector2 movement;
       private GameHandler gameHandler;

       // Dash vars
       public float dashSpeed = 20f;
       public float dashDuration = 0.2f;
       public float dashCooldown = 1f;
       private bool isDashing = false;
       private float dashTimeLeft;
       private float lastDashTime;

        // Knockback variables
        private bool isKnockedBack = false;
        private float knockbackDuration = 0.2f;
        private float knockbackTimer = 0f;

       // audio vars
       public AudioSource dash_sfx;

       // Auto-load the RigidBody component into the variable:
       void Start(){
             gameHandler = GameObject.FindObjectOfType<GameHandler>();
             rb = GetComponent<Rigidbody2D> ();
       }

       // Listen for player input to move the object:
       void FixedUpdate(){

             if (!isDashing && !isKnockedBack){
                   movement.x = Input.GetAxisRaw ("Horizontal");
                   //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
                   movement.y = Input.GetAxisRaw ("Vertical");
                   //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
             }
            else if (isKnockedBack)
            {
                knockbackTimer -= Time.fixedDeltaTime;
                if (knockbackTimer <= 0f)
                {
                    isKnockedBack = false;
                    rb.velocity = Vector2.zero; // stop sliding after knockback
                }
            }
             rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
             HandleDash();

       }
       void Update(){
             if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= lastDashTime + dashCooldown){
                   dash_sfx.Play();
                   StartDash();
             }
       }
       //should I be adding the multiplier to the dash speed? 
     void StartDash(){
             isDashing = true;
             dashTimeLeft = dashDuration;
             lastDashTime = Time.time;
             //scale dashspeed at half the rate of move speed so as to not be too egregious
             moveSpeed = dashSpeed * (((gameHandler.speedMultiplier - 1f) / 4f ) +1f);
       }
     void HandleDash()
     {
         if (isDashing){
             dashTimeLeft -= Time.fixedDeltaTime;
             if (dashTimeLeft <= 0){
                   isDashing = false;
                   moveSpeed = 5f * gameHandler.speedMultiplier; // Reset to normal speed
             }
       }
     }
     public void UpdateMoveSpeed(){
       // moveSpeed = moveSpeed / (gameHandler.speedMultiplier - 0.2f) * gameHandler.speedMultiplier;
       moveSpeed = 5f * gameHandler.speedMultiplier;
     }

        public void ApplyKnockback(Vector2 direction, float force)
        {
            isKnockedBack = true;
            knockbackTimer = knockbackDuration;
            rb.velocity = direction.normalized * force;
        }

 }

