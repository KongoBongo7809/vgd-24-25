using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    
    public Player red;
    public Player blue;
    public Player green;
    public Player purple;
    public Player[] colors;
    public int[] order = {0, 1, 2, 3};
    public int index = 0;
    public float timer = 0;
    public bool isCooperative;
    
    // Start is called before the first frame update
    void Start()
    {
        isCooperative = PlayerPrefs.GetString("gameMode") == "Cooperative";
        colors = new Player[] {red, blue, green, purple};
        Timer.Shuffle(order);

        if (isCooperative)
        {
            for (int i = 0; i < 4; i++)
            {
                colors[i].turnActive = (order[index] == i);
            }
        }
        else
        {
            red.turnActive = true;
            blue.turnActive = true;
            green.turnActive = true;
            purple.turnActive = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (isCooperative && (Input.GetKeyUp(KeyCode.Space) || timer > 60 / PlayerPrefs.GetInt("playerAmt")))
        {
            index++;
            timer = 0;
        }
    }
}
