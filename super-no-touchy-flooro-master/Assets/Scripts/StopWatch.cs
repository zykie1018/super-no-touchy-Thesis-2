using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopWatch : MonoBehaviour
{
    float timer;

    public float seconds;
    public float minutes;
    bool start;

    [SerializeField] Text timeText;
    void Start()
    {
        start = false;
        timer = 0;
    }

    void Update()
    {
        stopWatchCalcul();
    }

    void stopWatchCalcul()
    {
        if (!start)
        {
            timer += Time.deltaTime;
            seconds = (int)(timer % 60);
            minutes = (int)((timer % 60) % 60);

            timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }

    public void startTimer()
    {
        start = true;
    }
    public void pauseTimer()
    {
        start = false;
    }
    public void resetTimer()
    {
        start = false;
        timer = 0;
        timeText.text = "00:00";
    }
    public void newGameTimer()
    {
        start = true;
        timer = 0;
    }
}