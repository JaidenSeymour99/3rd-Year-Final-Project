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

    private bool noStart;

    void Start()
    {
        noStart = true;
        win = false;
        startTime = Time.time;
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
        win = true;
    }
}
