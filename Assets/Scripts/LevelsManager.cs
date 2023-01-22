using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsManager : MonoBehaviour
{
    public string NextLevelName;
    // Start is called before the first frame update
    void Start()
    {
        UpdateMaxScore();
    }

    public void UpdateMaxScore()
    {
        Text t = GameObject.FindWithTag("maxscores").GetComponent<Text>();
        t.text = PlayerPrefs.GetInt("MaxScore").ToString();
    }


    public void LoadNextLevel()
    {
        SceneManager.LoadScene(NextLevelName);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitted.");
    }

    // Navigation Menu Functions
    public void PlayPauseGame()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
