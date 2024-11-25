using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplitScreen : MonoBehaviour
{
    //Camera and border objects
    public Camera cam1, cam2, cam3, cam4;
    public RawImage borderHoriz, borderVert1, borderVert2;

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

            //Configure the borders
            borderVert1.gameObject.SetActive(true);
            borderVert2.gameObject.SetActive(true);
            borderHoriz.gameObject.SetActive(false);
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
        }
    }
}
