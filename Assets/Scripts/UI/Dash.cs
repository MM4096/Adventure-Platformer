using System;
using UnityEngine;
using UnityEngine.UI;


public class Dash : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image fill;
    [SerializeField] private Color loadFill;
    [SerializeField] private Color loadedFill;

    private void Awake()
    {
        if (PlayerPrefs.GetString("Preferences").Split(',')[1] == "0")
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (PlayerMovement.instance.dashTime > 0)
        {
            fill.color = loadedFill;
            if (PlayerMovement.instance.inWater)
            {
                slider.value = PlayerMovement.instance.dashTime;
            }
            else
            {
                slider.value = PlayerMovement.instance.dashTime / 0.2f;
            }
        }
        else if (PlayerMovement.instance.dashCountdown > 0)
        {
            fill.color = loadFill;
            if (PlayerMovement.instance.inWater)
            {
                slider.value = 1 - (PlayerMovement.instance.dashCountdown - Mathf.Clamp(PlayerMovement.instance.dashTime, 0, 2)) / 0.5f;
            }
            else
            {
                slider.value = 1 - (PlayerMovement.instance.dashCountdown - Mathf.Clamp(PlayerMovement.instance.dashTime, 0, 0.2f)) / 1.3f;
            }
        }
        else
        {
            fill.color = loadedFill;
            slider.value = 1;
        }
    }
}