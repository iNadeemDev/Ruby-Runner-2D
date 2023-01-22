using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour 
{
    private int scores;
    private int life;

    // Making Singleton
    private static ScoreManager instance;
    public static ScoreManager GetInstance()
    {
        if(instance == null)
        {
            instance = new ScoreManager();
            return instance;
        }
        else
        {
            return instance;
        }
    }

    // Scores setter
    public void AddScores(int scores)
    {
        this.scores += scores;
    }

    // Score getter
    public int GetScores()
    {
        return this.scores;
    }

    // Life setter
    public void LifeStter(int life)
    {
        this.life += life;
    }

    // Life getter
    public int LifeGetter()
    {
        return this.life;
    }

    public void UpdateScoreUI()
    {
        Text t = GameObject.FindWithTag("scores").GetComponent<Text>();
        t.text = this.scores.ToString();
    }


}
