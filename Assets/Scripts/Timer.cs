using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    public static bool startRace = false;
    private static bool win = false;
    private static float t;

    private bool noStart;

    void Start()
    {
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
            float t = Time.time - startTime;

            string minutes = ((int) t / 60).ToString();
            string seconds = (t % 60).ToString("f2");
            
            timerText.text = minutes + ":" + seconds;
        }
    }

    public static void Win()
    {
        if(t <= 17)
        {
            Player.score += 2000;
        }
        if(t <= 20 && t > 17)
        {
            Player.score += 1200;
        }
        if(t <= 30 && t > 20)
        {
            Player.score += 800;
        }
        if(t <= 40 && t > 30)
        {
            Player.score += 600;
        }
        if(t <= 50 && t > 40)
        {
            Player.score += 400;
        }
        if(t <= 60 && t > 50)
        {
            Player.score += 300;
        }
        if(t > 60)
        {
            Player.score += 200;
        }
        win = true;
    }
}
