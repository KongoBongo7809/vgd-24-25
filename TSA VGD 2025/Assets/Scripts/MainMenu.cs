using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Set the amount of players
    public void setPlayerAmt(int amt)
    {
        PlayerPrefs.SetInt("playerAmt", amt);
        Debug.Log(PlayerPrefs.GetInt("playerAmt"));
    }

    //Change scenes
    public void changeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
