using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SplitScreen : MonoBehaviour
{
    //Camera and border objects
    public Player p3;
    public Camera cam1, cam2, cam3, cam4;
    public RawImage borderHoriz, borderVert1, borderVert2;
    public GameObject dollarLeft, dollarCenter, dollarRight;
    public GameObject pizzaLeft, pizzaCenter, pizzaRight;
    public TextMeshProUGUI dollarTextCenter, pizzaTextCenter;

    private void Start()
    {
        //Two player setup
        if (PlayerPrefs.GetInt("playerAmt") == 2)
        {
            //Disable Player 3 and Player 4
            cam3.transform.parent.gameObject.SetActive(false);
            cam4.transform.parent.gameObject.SetActive(false);

            //Create rectangles for camera viewing
            cam1.rect = new Rect(0, 0, 0.5f, 1);
            cam2.rect = new Rect(0.5f, 0, 0.5f, 1);

            //Adjust camera zoom
            cam1.orthographicSize = 8f;
            cam2.orthographicSize = 8f;

            //Configure the borders
            borderVert1.gameObject.SetActive(true);
            borderVert2.gameObject.SetActive(true);
            borderHoriz.gameObject.SetActive(false);

            //Disable dollar counters
            dollarLeft.SetActive(false);
            dollarCenter.SetActive(false);
            dollarRight.SetActive(false);

            //Disable pizza counters
            pizzaLeft.SetActive(false);
            pizzaCenter.SetActive(false);
            pizzaRight.SetActive(false);
        }
        //Three player setup
        else if(PlayerPrefs.GetInt("playerAmt") == 3)
        {
            //Disable Player 4
            cam4.transform.parent.gameObject.SetActive(false);

            //Create rectangles for camera viewing
            cam1.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
            cam2.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            cam3.rect = new Rect(0.25f, 0, 0.5f, 0.5f);

            //Configure the borders
            borderVert1.gameObject.SetActive(true);
            borderVert2.gameObject.SetActive(false);
            borderHoriz.gameObject.SetActive(true);

            //Disable dollar counters
            dollarLeft.SetActive(false);
            dollarCenter.SetActive(true);
            dollarRight.SetActive(false);
            p3.dollarCounter = dollarTextCenter;

            //Disable pizza counters
            pizzaLeft.SetActive(false);
            pizzaCenter.SetActive(true);
            pizzaRight.SetActive(false);
            p3.pizzaCounter = pizzaTextCenter;
                
        }
        //Four player setup
        else if(PlayerPrefs.GetInt("playerAmt") == 4) {

            //Create rectangles for camera viewing
            cam1.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
            cam2.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            cam3.rect = new Rect(0, 0, 0.5f, 0.5f);
            cam4.rect = new Rect(0.5f, 0, 0.5f, 0.5f);

            //Configure the borders
            borderVert1.gameObject.SetActive(true);
            borderVert2.gameObject.SetActive(true);
            borderHoriz.gameObject.SetActive(true);

            //Disable dollar counters
            dollarLeft.SetActive(true);
            dollarCenter.SetActive(false);
            dollarRight.SetActive(true);

            //Disable pizza counters
            pizzaLeft.SetActive(true);
            pizzaCenter.SetActive(false);
            pizzaRight.SetActive(true);
        }
    }
}
