using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Menu : MonoBehaviour
{
    private readonly string[] HINT_TEXT = new[]
    {
        "Press jump in air to double jump!", "Space to dash!", "'C' to look like a smart person!",
        "Wall jump was an intentional feature... trust", "Finish go green when you can finish!", 
        "Beat 2.752 in freeplay!", "Why? Why not!", "Beans", "Gotta go fast!", "Why's the map so big?",
        "Oh, where did that rock come from?", "Touching the finish gives 3% completion"
    };

    [SerializeField] private GameObject menu;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private GameObject hintText;
    [SerializeField] private TextMeshProUGUI loadingProgress;
    private float counter = 5f;
    private bool loading;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            loadGameButton.interactable = true;
        }
        else
        {
            loadGameButton.interactable = false;
        }
        
        if (!PlayerPrefs.HasKey("Preferences"))
        {
            PlayerPrefs.SetString("Preferences", "1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1");
        }
    }

    public void StartGame(bool load)
    {
        loading = true;
        menu.SetActive(true);
        PlayerPrefs.SetString("mode", load ? "t" : "f");
        hintText.GetComponent<TextMeshProUGUI>().text = HINT_TEXT[Random.Range(0, HINT_TEXT.Length - 1)];
        StartCoroutine(StartLevel());
    }

    private void Update()
    {
        if (loading && asyncLoad.progress < 0.9f)
        {
            counter -= Time.deltaTime;
            if (counter < 0) {
                hintText.GetComponent<TextMeshProUGUI>().text = HINT_TEXT[Random.Range(0, HINT_TEXT.Length - 1)];
                counter = 5f;   
            }
            loadingProgress.text = "Loading: " + Mathf.RoundToInt(asyncLoad.progress * 100) + "%";
        }
        else if (loading && asyncLoad.progress < 1f)
        {
            loadingProgress.text = "Scene loaded. Waiting for FPS to reach target...";
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Menu"));
            StartCoroutine(ActivateScene());
        }
        else if (loading && asyncLoad.progress >= 1f)
        {
            SceneManager.UnloadSceneAsync("Menu");
        }
    }

    AsyncOperation asyncLoad;

    private IEnumerator ActivateScene()
    {
        asyncLoad.allowSceneActivation = true;
        yield return null;
    }
    
    private IEnumerator StartLevel()
    {
        asyncLoad = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;
        
        yield return asyncLoad;
    }

    public void Tutorial()
    {
        SceneManager.LoadSceneAsync("Tutorial");
    }
}
