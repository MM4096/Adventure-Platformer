using System;
using UnityEngine;


public class Trigger : MonoBehaviour
{
    [SerializeField] private Speedrun speedrunController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            speedrunController.StartTimer();
            Achievements.instance.SpeedrunStarted();
        }
    }
}