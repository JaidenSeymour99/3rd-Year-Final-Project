using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class Timer : MonoBehaviour
{

    public Text timerText;
    private float startTime;
    private bool win = false;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (win) {
            return;
        }
        else
        {    

            float t = Time.time - startTime;

            string minutes = ((int) t / 60).ToString();
            string seconds = (t % 60).ToString("f2");
            
            timerText.text = minutes + ":" + seconds;
        }
    }

    public void Win()
    {
        win = true;
    }
}
