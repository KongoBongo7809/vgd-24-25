using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    //Set the amount of players
    public void setPlayerAmt(int amt)
    {
        PlayerPrefs.SetInt("playerAmt", amt);
    }

    //Set the game mode
    public void setGameMode(string gameMode)
    {
        PlayerPrefs.SetString("gameMode", gameMode);
    }

    //Next scenes
    public void nextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Previous scene
    public void previousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    //Exit game
    public void exit()
    {
        Application.Quit();
    }
}
