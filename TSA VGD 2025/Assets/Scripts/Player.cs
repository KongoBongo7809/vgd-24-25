using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Movement
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    Vector2 movement;
    public char[] keybinds;
    //Animation
    public Animator animator;
    
    private void Update()
    {
        //Set the movement vector based on inputs
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //Adjusting the player sprite animation based on the direction of the player
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        //Move the rigidbody accordingly with speed and time factored in
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

}
