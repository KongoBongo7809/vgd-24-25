using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizzeria : MonoBehaviour
{
    public int time = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Pizza(collision.gameObject.GetComponent<Player>()));
        }
    }

    IEnumerator Pizza(Player p)
    {
        while (p.pizzas < 3)
        {
            yield return new WaitForSeconds(time);
            p.pizzas++;
        }
    }
}
