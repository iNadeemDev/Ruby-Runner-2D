using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject[] Levels;
    public GameObject[] CanvasGameObjects;
    public int LevelNumber;

    public static LevelManager instance;

    private void Awake()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        DisableAllCanvasObjects();
        CanvasGameObjects[0].SetActive(true);
    }

    private void Update()
    {
        if (Player.isDead)
        {
            YouLostGameUI();
        }
    }

    private void DisableAllLevels()
    {
        foreach (GameObject level in Levels)
        {
            level.SetActive(false);
        }
    }

    private void DisableAllCanvasObjects()
    {
        foreach (GameObject obj in CanvasGameObjects)
        {
            obj.SetActive(false);
        }
    }

    public void LoadNextLevel()
    {
        DisableAllLevels();
        Levels[LevelNumber].SetActive(true);
        LevelNumber++;
    }

    public void StartGame()
    {
        DisableAllCanvasObjects();
        CanvasGameObjects[1].SetActive(true);
        CanvasGameObjects[2].SetActive(true);
        LoadNextLevel();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("RubyRunner");
    }

    public void YouLostGameUI()
    {
        CanvasGameObjects[3].SetActive(true);
    }

    public void PlayLevelAgain()
    {
        CanvasGameObjects[3].SetActive(false);
        MainMenu();
        DisableAllLevels();
        Levels[LevelNumber - 1].SetActive(true);
    }
}
