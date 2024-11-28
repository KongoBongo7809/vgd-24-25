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

    //Collision
    public BoxCollider2D box;

    private void Update()
    {
        //Set the movement vector based on inputs
        movement.x = Input.GetAxisRaw("Horizontal " + color);
        movement.y = Input.GetAxisRaw("Vertical " + color);

        //Set animations according to user inputs and speed
        if (movement.sqrMagnitude != 2)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
        animator.SetFloat("Speed", movement.sqrMagnitude);
        animator.enabled = animator.GetFloat("Speed") > 0.05;

        //Adjust box colliders according to direction player is facing
        if (Mathf.Abs(movement.x) < Mathf.Abs(movement.y) && movement.sqrMagnitude != 0)
        {
            box.size = new Vector2(1.5f, 2.5f);
            box.offset = new Vector2(0, 0);
        }
        else if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y) && movement.sqrMagnitude != 0)
        {
            box.size = new Vector2(2.7f, 1.3f);
            box.offset = new Vector2(0f, -0.35f);
        }
    }

    private void FixedUpdate()
    {
        //Move the rigidbody accordingly with speed and time factored in
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}