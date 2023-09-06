using System;
using TMPro;
using UnityEngine;


public class Countdown : MonoBehaviour
{
    public static Countdown instance;
    [SerializeField] private TextMeshProUGUI countdownText;
    private float countdownTime;
    private float colorChange;
    
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (countdownTime > 0)
        {
            countdownTime -= Time.deltaTime;
            string sCountdown = Mathf.Clamp(Mathf.Round(countdownTime * 100) / 100, 0, Mathf.Infinity).ToString();
            while (sCountdown.Split(".").Length < 2)
            {
                sCountdown += ".0";
            }
            countdownText.text = sCountdown;
            if (countdownTime < colorChange)
            {
                countdownText.color = Color.green;
            }
            else
            {
                countdownText.color = Color.yellow;
            }
        }
        else
        {
            countdownText.text = "";
        }
    }

    public void StartCountdown(float time, float change)
    {
        countdownTime = time;
        colorChange = change;
    }
}