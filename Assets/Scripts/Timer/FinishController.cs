using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class FinishController : MonoBehaviour
{
    public static FinishController instance;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject back;
    [HideInInspector] public float completionPercentage;
    private int normalObstacles;
    private int goldObstacles;
    private float completePercentage;
    private bool isComplete;
    [SerializeField] private GameObject finishScreen;
    [SerializeField] private TextMeshProUGUI finishText;

    private void Awake()
    {
        instance = this;
        foreach (Collectable collectable in FindObjectsOfType<Collectable>())
        {
            if (collectable.GameObject().GetComponent<Collectable>().type == CollectableType.Gold)
            {
                goldObstacles++;
            }
            else
            {
                normalObstacles++;
            }
        }
    }
    
    public int CalculatePercentage()
    {
        int goldLeft = 0;
        int normalLeft = 0;
        
        foreach (Collectable collectable in FindObjectsOfType<Collectable>())
        {
            if (collectable.GameObject().GetComponent<Collectable>().type == CollectableType.Gold)
            {
                goldLeft++;
            }
            else
            {
                normalLeft++;
            }
        }
        
        int goldCollected = goldObstacles - goldLeft;
        int normalCollected = normalObstacles - normalLeft;

        float percentage = 0;
        percentage += (float) goldCollected / goldObstacles * 50;
        percentage += (float) normalCollected / normalObstacles * 50;
        return Mathf.RoundToInt(percentage);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Collected " + CalculatePercentage() + "%";
        if (CalculatePercentage() >= completionPercentage)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && CalculatePercentage() >= completionPercentage - 3)
        {
            Timer.instance.Stop();
            back.SetActive(true);
            isComplete = true;
            other.gameObject.GetComponent<Rigidbody2D>().simulated = false;
            finishScreen.SetActive(true);
            finishText.text = "You completed the level in " + Timer.instance.FormatTime() + "<br><br>" +
                              "Completed " + CalculatePercentage() + "%<br>";
            if (CalculatePercentage() == 0)
            {
                finishText.text += "<br><br>I wonder why the map is so big...";
            }
            else if (CalculatePercentage() == 100)
            {
                finishText.text += "<br><br>Great job! Now beat your time!";
            }
            SaveSystem.instance.Save();
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }
}
