using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    public Player red, blue, green, purple;
    public Player[] colors;
    public GameObject[] bars;
    public int[] order;
    public int index = 0;
    public float timer = 0;
    public bool isCooperative;
    public int playerAmt;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAmt = PlayerPrefs.GetInt("playerAmt");
        switch (playerAmt)
        {
            case 2:
                colors = new Player[] {red, blue};
                order = Timer.Shuffle(new int[] {0, 1});
                break;
            case 3:
                colors = new Player[] {red, blue, green};
                order = Timer.Shuffle(new int[] {0, 1, 2});
                break;
            case 4:
                colors = new Player[] {red, blue, green, purple};
                order = Timer.Shuffle(new int[] {0, 1, 2, 3});
                break;
        }
        
        isCooperative = PlayerPrefs.GetString("gameMode") == "Cooperative";
        if (isCooperative)
        {
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i].turnActive = (order[index] == i);
            }
            ColorBars();
        }
        else
        {
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i].turnActive = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (isCooperative && !Timer.leaderboardShown && (Input.GetKeyUp(KeyCode.Space) || timer > 60 / playerAmt))
        {
            index++;
            index %= order.Length;
            timer = 0;
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i].turnActive = (order[index] == i);
            }
            ColorBars();
        }
    }

    public Color32 GetColor(int index)
    {
        switch (index)
        {
            case 0:
                return new Color32(204, 85, 61, 255);
            case 1:
                return new Color32(56, 92, 235, 255);
            case 2:
                return new Color32(34, 177, 76, 255);
            case 3:
                return new Color32(139, 61, 191, 255);
        }
        
        return new Color32(0, 0, 0, 0);
    }

    public void ColorBars()
    {
        foreach (GameObject bar in bars)
        {
            bar.GetComponent<RawImage>().color = GetColor(order[index]);
        }
    }
}
