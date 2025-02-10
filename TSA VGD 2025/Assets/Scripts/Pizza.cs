using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    public Transform target;
    public Transform player;
    public LayerMask houseLayer;

    public float speed = 5f;
    private Rigidbody2D rb;
    private float angle;
    private Vector3 dir;
    public bool bakedPizza;

    public void setPizza (Transform t, Transform p, bool bP)
    {
        target = t;
        player = p;
        bakedPizza = bP;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Physics.IgnoreCollision(target.transform.GetComponent<Collider>(), transform.GetComponent<Collider>());

        /*dir = target.position - player.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/
    }

    // Update is called once per frame
    void Update()
    {
        //rb.MovePosition(dir * speed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided Almost");
        if (collision.gameObject.layer == 10 && !bakedPizza)
        {
            player.GetComponent<Player>().UpdatePoints(10);
            player.GetComponent<Player>().pizzas--;
            player.GetComponent<Player>().deliveringPizza = false;
            FindObjectOfType<AudioManager>().Play("Cash");
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player" && bakedPizza)
        {
            Debug.Log("Collided");
            target.GetComponent<Player>().pizzas++;
            target.GetComponent<Player>().deliveringPizza = false;
            FindObjectOfType<AudioManager>().Play("Cash");
            Destroy(gameObject);
        }
    }
}
