using System;
using UnityEngine;


public class Pause : MonoBehaviour
{
    public static Pause instance;
    [SerializeField] private GameObject pauseMenu;
    [HideInInspector] public bool paused;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }

        pauseMenu.SetActive(paused);
        Time.timeScale = paused ? 0 : 1;
    }
    
    public void Resume()
    {
        paused = false;
    }
}