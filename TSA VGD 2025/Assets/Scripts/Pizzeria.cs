using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizzeria : MonoBehaviour
{
    public int time = 5;
    public GameObject pizza;

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
        int temp = 3 - p.pizzas;
        while (p.animator.enabled)
        {
            yield return new WaitForEndOfFrame();
        }
        if (Physics2D.Distance(p.GetComponent<Collider2D>(), transform.GetComponent<Collider2D>()).isOverlapped)
        {
            while (temp > 0)
            {
                p.deliveringPizza = true;
                yield return new WaitForSeconds(time);
                GameObject newPizza = Instantiate(pizza, transform);
                newPizza.GetComponent<Pizza>().setPizza(p.transform, transform, true);
                temp--;
            }
        }
        
    }

}
