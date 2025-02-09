using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI firstPlace;
    [SerializeField] TextMeshProUGUI secondPlace;
    [SerializeField] TextMeshProUGUI thirdPlace;
    [SerializeField] TextMeshProUGUI fourthPlace;
    [SerializeField] float remainingTime;

    public GameObject redCar;
    public GameObject blueCar;
    public GameObject greenCar;
    public GameObject purpleCar;

    TextMeshProUGUI[] leaderboardText;

    public GameObject[] uiDisable;
    public GameObject[] uiEnable;

    private int[] leaderboard = new int[4];
    public Player red;
    public Player blue;
    public Player green;
    public Player purple;

    private void Start()
    {
        leaderboardText = new TextMeshProUGUI[] {firstPlace, secondPlace, thirdPlace, fourthPlace};
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime < 0) remainingTime = 0;
        }
        else
        {
            remainingTime = 0;
            EndRound();
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        //Set Leaderboard
        leaderboard = new int[] { red.points, blue.points, green.points, purple.points };
    }

    
    public static int[] Shuffle(int[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int j = UnityEngine.Random.Range(i, n);
            Swap(array, i, j);
        }
        return array;
    }

    public static void Swap(int[] array, int i, int n)
    {
        int temp = array[i];
        array[i] = array[n];
        array[n] = temp;
    }
    

    public void EndRound()
    {
        
        int[] places = Shuffle(new int[] {0, 1, 2, 3});
        if (leaderboard[places[0]] < leaderboard[places[1]]) Swap(places, 0, 1);
        if (leaderboard[places[2]] < leaderboard[places[3]]) Swap(places, 2, 3);
        if (leaderboard[places[0]] < leaderboard[places[2]]) Swap(places, 0, 2);
        if (leaderboard[places[1]] < leaderboard[places[3]]) Swap(places, 1, 3);
        if (leaderboard[places[1]] < leaderboard[places[2]]) Swap(places, 1, 2);

        foreach (GameObject g in uiDisable)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in uiEnable)
        {
            g.SetActive(true);
        }

        foreach (int i in places)
        {
            GameObject color = returnColor(i);
            Debug.Log(color);
            if (color == null)
            {
                continue;
            }
            RectTransform rect = color.transform.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(-65, 100 * (i+1) - 150);
            leaderboardText[i].text = leaderboard[i].ToString();
        }
        //return places;
    }

    public GameObject returnColor(int i)
    {
        int playerAmt = PlayerPrefs.GetInt("playerAmt");
        Debug.Log(i);
        switch (i)
        {
            case 0:
                return redCar;
            case 1:
                return blueCar;
            case 2:
                return playerAmt > 2 ? greenCar : null;
            case 3:
                return playerAmt > 3 ? purpleCar: null;
            default:
                return null;
        }
    }
}
