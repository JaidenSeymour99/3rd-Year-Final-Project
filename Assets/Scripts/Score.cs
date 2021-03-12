using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;

    void Update()
    {
        // used to show the players score.
        ShowScore();
    }
    public void ShowScore()
    {
        // Debug.Log(score);
        scoreText.text = "Score: " + Player.score.ToString();
        
        
    }
}
