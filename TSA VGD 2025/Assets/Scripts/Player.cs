using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Movement
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    Vector2 movement;
    public string color;
    //Animation
    public Animator animator;

    private void Update()
    {
        //Set the movement vector based on inputs
        movement.x = Input.GetAxisRaw("Horizontal " + color);
        movement.y = Input.GetAxisRaw("Vertical " + color);

        //Set animations according to user inputs and speed
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        animator.enabled = animator.GetFloat("Speed") > 0.05;
    }

    private void FixedUpdate()
    {
        //Move the rigidbody accordingly with speed and time factored in
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

}