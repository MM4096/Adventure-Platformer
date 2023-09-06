using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TutorialFinisher : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadSceneAsync("Menu");
        }
    }
}