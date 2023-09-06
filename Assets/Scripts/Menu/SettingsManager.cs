using System;
using UnityEngine;
using UnityEngine.UI;


public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Toggle timer;
    [SerializeField] private Toggle dash;
    [SerializeField] private Toggle particles;
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Preferences"))
        {
            PlayerPrefs.SetString("Preferences", "1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1");
        }
    }
    
    private void Start()
    {
        string[] preferences = PlayerPrefs.GetString("Preferences").Split(',');
        timer.isOn = preferences[0] == "1";
        dash.isOn = preferences[1] == "1";
        particles.isOn = preferences[2] == "1";
    }

    private void Update()
    {
        string[] preferences = PlayerPrefs.GetString("Preferences").Split(',');
        preferences[0] = timer.isOn ? "1" : "0";
        preferences[1] = dash.isOn ? "1" : "0";
        preferences[2] = particles.isOn ? "1" : "0";
        PlayerPrefs.SetString("Preferences", string.Join(",", preferences));
    }
}