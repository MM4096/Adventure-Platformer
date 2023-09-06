using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public float timeElapsed = 0f;
    [SerializeField] private TextMeshProUGUI timer;
    private bool started;

    private bool isRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            timeElapsed += Time.deltaTime;
            timeElapsed = Mathf.Round(timeElapsed * 1000) / 1000;
            timer.text = FormatTime();
        }
        if (PlayerPrefs.GetString("Preferences").Split(',')[0] == "0")
        {
            timer.text = "";
        }
    }

    public string FormatTime()
    {
        int elapsedMinutes = Mathf.FloorToInt(timeElapsed / 60);
        float elapsedSeconds = timeElapsed % 60;
        int iElapsedSeconds = Mathf.FloorToInt(elapsedSeconds);
        int elapsedMilliseconds = Mathf.FloorToInt((elapsedSeconds - iElapsedSeconds) * 1000);
        string sElapsedMilliseconds = elapsedMilliseconds.ToString();
        if (elapsedMilliseconds < 10)
        {
            sElapsedMilliseconds = "00" + elapsedMilliseconds;
        }
        else if (elapsedMilliseconds < 100)
        {
            sElapsedMilliseconds = "0" + elapsedMilliseconds;
        }
        string timeElapsedString;
        if (elapsedSeconds < 10)
        {
            timeElapsedString = elapsedMinutes + ":0" + iElapsedSeconds + "." + sElapsedMilliseconds;
        }
        else
        {
            timeElapsedString = elapsedMinutes + ":" + iElapsedSeconds + "." + sElapsedMilliseconds;
        }
        return timeElapsedString;
    }

    public void Stop()
    {
        isRunning = false;
    }

    public void StartTimer()
    {
        if (!started)
        {
            started = true;
            isRunning = true;   
        }
    }

    public float GetTime()
    {
        return timeElapsed;
    }
}
