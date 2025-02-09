using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    //Movement
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    Vector2 movement;
    public string color;
    public bool turnActive;

    //Animation
    public Animator animator;

    //Collision
    public BoxCollider2D box;

    //Points
    public int points = 0;
    public TextMeshProUGUI dollarCounter;

    //Particles

    private void Update()
    {
        //Set the movement vector based on inputs
        if (Timer.leaderboardShown || !turnActive)
        {
            movement.x = 0;
            movement.y = 0;
        }
        else
        {
            movement.x = Input.GetAxisRaw("Horizontal " + color);
            movement.y = Input.GetAxisRaw("Vertical " + color);
        }

        //Set animations according to user inputs and speed
        if (movement.sqrMagnitude != 2)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
        animator.SetFloat("Speed", movement.sqrMagnitude);
        animator.enabled = animator.GetFloat("Speed") > 0.05;

        /*if (animator.enabled)
        {
            GetComponent<ParticleSystem>().Play();
        }
        else
        {
            GetComponent<ParticleSystem>().Stop();
        }*/

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

        //Update point counter
        dollarCounter.SetText(points.ToString());
    }

    private void FixedUpdate()
    {
        //Move the rigidbody accordingly with speed and time factored in
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void UpdatePoints(int amt)
    {
        points += amt;
    }
}