using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class playerMove : MonoBehaviour {

      public Rigidbody2D rb;
      public float moveSpeed = 5f;
      public Vector2 movement;

      // Dash vars
      public float dashSpeed = 20f;
      public float dashDuration = 0.2f;
      public float dashCooldown = 1f;
      private bool isDashing = false;
      private float dashTimeLeft;
      private float lastDashTime;

      public AudioSource dash_sfx;

      // Auto-load the RigidBody component into the variable:
      void Start(){
            rb = GetComponent<Rigidbody2D> ();
      }

      // Listen for player input to move the object:
      void FixedUpdate(){

            if (!isDashing){
                  movement.x = Input.GetAxisRaw ("Horizontal");
                  //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
                  movement.y = Input.GetAxisRaw ("Vertical");
                  //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
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

    void StartDash(){
            isDashing = true;
            dashTimeLeft = dashDuration;
            lastDashTime = Time.time;
            moveSpeed = dashSpeed;
      }

    void HandleDash()
    {
        if (isDashing){
            dashTimeLeft -= Time.fixedDeltaTime;
            if (dashTimeLeft <= 0){
                  isDashing = false;
                  moveSpeed = 5f; // Reset to normal speed
            }
      }
    }

}