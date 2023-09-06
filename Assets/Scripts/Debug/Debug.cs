using System;
using TMPro;
using UnityEngine;


public class Debug : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private TextMeshProUGUI debugText;

    private bool isVisible;

    private void Update()
    {
        menu.SetActive(true);
        if (Input.GetKeyDown(PlayerMovement.instance.keys[5]))
        {
            isVisible = !isVisible;
        }
        
        menu.SetActive(isVisible);
        
        debugText.text = "x velocity: " + Mathf.Round(PlayerMovement.instance.velocity.x * 100) / 100;
        debugText.text += "<br>y velocity: " + Mathf.Round(PlayerMovement.instance.velocity.y * 100) / 100;
        debugText.text += "<br>double jump: " + PlayerMovement.instance.canDoubleJump;
        debugText.text += "<br>dash: " + (PlayerMovement.instance.dashCountdown <= 0);
        debugText.text += "<br>coyote: " + (PlayerMovement.instance.coyoteTime > 0);
        debugText.text += "<br>water: " + PlayerMovement.instance.inWater;
        debugText.text += "<br>isDashing: " + (PlayerMovement.instance.dashTime > 0);
    }
}