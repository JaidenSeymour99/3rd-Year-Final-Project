using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private float finishTime;
    public static float current;
    public static bool startRace = false;
    private static bool win = false;
    private static float t = 0;
    

    private bool noStart;

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        current = currentScene.buildIndex;

        t = 0;
        noStart = true;
        win = false;
        startTime = Time.time;
        startRace = false;
    }

    void Update()
    {
        
        
        if (win) {
            return;
        }
        if(startRace && noStart)
        {
            startTime = Time.time;
            noStart = false;
        }
        if(startRace)
        {   
            t = Time.time - startTime;
            string minutes = ((int) t / 60).ToString();
            string seconds = (t % 60).ToString("f2");
            
            timerText.text = minutes + ":" + seconds;

            //finishTime = t - startTime;
            //Debug.Log(finishTime);
        }
        
    }

    public static void Win()
    {
        
        // Debug.Log(current);
        if(current == 1)
        {
            if(t > 60)
            {
                Player.score *= 1.1;
            }
            else if(t <= 60 && t > 50)
            {
                Player.score *= 1.2;
            }
            else if(t <= 50 && t > 40)
            {
                Player.score *= 1.3;
            }
            else if(t <= 40 && t > 30)
            {
                Player.score *= 1.4;
            }
            else if(t <= 30 && t > 20)
            {
                Player.score *= 1.5;
            }
            else if(t <= 20 && t > 17)
            {
                Player.score *= 2;
            }        
            else if(t <= 17 && t > 15)
            {
                Player.score *= 3;
            }
            else if(t <= 15 && t > 0)
            {
                Player.score *= 4;
            }
        }
        if(current == 2)
        {
            if(t > 60)
            {
                Player.score *= 1.1;
            }
            else if(t <= 60 && t > 50)
            {
                Player.score *= 1.2;
            }
            else if(t <= 50 && t > 40)
            {
                Player.score *= 1.3;
            }
            else if(t <= 40 && t > 30)
            {
                Player.score *= 1.4;
            }
            else if(t <= 30 && t > 20)
            {
                Player.score *= 1.5;
            }
            else if(t <= 20 && t > 17)
            {
                Player.score *= 1.7;
            }        
            else if(t <= 17 && t > 15)
            {
                Player.score *= 2;
            }
            else if(t <= 15 && t > 0)
            {
                Player.score *= 3;
            }
        }

        win = true;
    }
}
