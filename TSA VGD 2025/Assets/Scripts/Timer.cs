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

    private int[] leaderboard = new int[4];
    public Player red;
    public Player blue;
    public Player green;
    public Player purple;

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
        leaderboard = new int[] {red.points, blue.points, green.points, purple.points};
    }

    public static int[] Shuffle(int[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int j = Random.Range(i, n);
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

    public int[] EndRound()
    {
        int[] places = Shuffle(new int[] {0, 1, 2, 3});
        if (leaderboard[places[0]] > leaderboard[places[1]]) Swap(places, 0, 1);
        if (leaderboard[places[2]] > leaderboard[places[3]]) Swap(places, 2, 3);
        if (leaderboard[places[0]] > leaderboard[places[2]]) Swap(places, 0, 2);
        if (leaderboard[places[1]] > leaderboard[places[3]]) Swap(places, 1, 3);
        if (leaderboard[places[1]] > leaderboard[places[2]]) Swap(places, 1, 2);
        
        firstPlace.text = leaderboard[places[3]].ToString();
        secondPlace.text = leaderboard[places[2]].ToString();
        thirdPlace.text = leaderboard[places[1]].ToString();
        fourthPlace.text = leaderboard[places[0]].ToString();

        return places;
    }
}
