using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Achievements : MonoBehaviour
{
    public static Achievements instance;
    [SerializeField] private RectTransform achievementParent;
    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private GameObject achievementBackground;
    [SerializeField] private TextMeshProUGUI achievementText;
    [SerializeField] private Color commonColor;
    [SerializeField] private Color rareColor;
    [SerializeField] private Color epicColor;
    [SerializeField] private Color legendaryColor;
    
    private bool isVisible;
    private float countdown;

    private int deaths;

    private Achievement climbAchievement;
    private Achievement collectableAchievement50;
    private Achievement collectableAchievement100;
    private Achievement deathAchievement1;
    private Achievement deathAchievement50;
    private Achievement borderAchievement;
    
    public Achievement[] achievements;
    
    private void Awake()
    {
        instance = this;
        climbAchievement = new Achievement("High Climber", "Reach a height of 100 on the border", AchievementType.Rare, false);
        collectableAchievement50 = new Achievement("Collector", "Collect 50% of the collectables", AchievementType.Rare, false);
        collectableAchievement100 = new Achievement("Mass Collector", "Collect 100% of the collectables", AchievementType.Legendary, false);
        deathAchievement1 = new Achievement("Touched Lava", "You can die in this game?", AchievementType.Common, false);
        deathAchievement50 = new Achievement("Master of Death", "Die 50 times", AchievementType.Epic, false);
        borderAchievement = new Achievement("That border", "What did you think was here?", AchievementType.Rare, false);
        achievements = new [] {climbAchievement, collectableAchievement50, collectableAchievement100, deathAchievement50, borderAchievement, deathAchievement1};
    }

    private void Update()
    {
        countdown -= Time.deltaTime;
        if (isVisible && countdown <= 0)
        {
            achievementPanel.SetActive(false);
            isVisible = false;
        }

        #region Achievements

        Transform player = GameObject.Find("Player").transform;
        
        if (player.position.y > 100 && player.position.x < -150 && !climbAchievement.unlocked)
        {
            ShowAchievement("High Climber", rareColor);
            climbAchievement.unlocked = true;
        }
        
        if (FinishController.instance.CalculatePercentage() > 97 && !collectableAchievement100.unlocked)
        {
            ShowAchievement("Mass Collector", legendaryColor);
            collectableAchievement100.unlocked = true;
        }
        
        if (FinishController.instance.CalculatePercentage() > 49 && !collectableAchievement50.unlocked)
        {
            ShowAchievement("Collector", rareColor);
            collectableAchievement50.unlocked = true;
        }

        if (player.transform.position.x > 346 && !borderAchievement.unlocked)
        {
            ShowAchievement("That border", rareColor);
            borderAchievement.unlocked = true;
        }

        #endregion
    }
    
    private void ShowAchievement(string achievementName, Color color)
    {
        achievementPanel.SetActive(true);
        achievementBackground.GetComponent<Image>().color = color;
        achievementText.text = achievementName;
        isVisible = true;
        countdown = 3;
    }

    public void AddDeath()
    {
        deaths++;
        if (deaths > 0 && !deathAchievement1.unlocked)
        {
            deathAchievement1.unlocked = true;
            ShowAchievement("Touched Lava", commonColor);
        }
        if (deaths > 49 && !deathAchievement50.unlocked)
        {
            deathAchievement50.unlocked = true;
            ShowAchievement("Master of Death", epicColor);
        }
    }

    public void LoadAchievements()
    {
        foreach (Transform child in achievementParent)
        {
            Destroy(child.gameObject);
        }

        
        InstantiateAchievement("Touched Lava", commonColor, "You can die in this game?", deathAchievement1.unlocked);
        InstantiateAchievement("That border", rareColor, "What did you think was here?", borderAchievement.unlocked);
        InstantiateAchievement("Collector", rareColor, "Complete Collected: 50%", collectableAchievement50.unlocked);
        InstantiateAchievement("High Climber", rareColor, "Reach a height of 100 on the border", climbAchievement.unlocked);
        InstantiateAchievement("Master of Death", epicColor, "Die 50 times", deathAchievement50.unlocked);
        InstantiateAchievement("Mass Collector", legendaryColor, "Collect all of the collectables", collectableAchievement100.unlocked);
    }

    private void InstantiateAchievement(string achievementName, Color color, string description, bool achieved)
    {
        Transform achievement = Instantiate(Resources.Load("Achievement") as GameObject, achievementParent).transform;
        if (achieved)
        {
            achievement.GetComponent<TooltipHoverController>().tooltipText = description;
            achievement.Find("Back").GetComponent<Image>().color = color;
            achievement.Find("Text").GetComponent<TextMeshProUGUI>().text = achievementName;   
        }
        else
        {
            achievement.GetComponent<TooltipHoverController>().tooltipText = "???";
            achievement.Find("Back").GetComponent<Image>().color = Color.grey;
            achievement.Find("Text").GetComponent<TextMeshProUGUI>().text = "???";
        }
    }

    public void UnpackAchievements(Achievement[] _achievements)
    {
        climbAchievement = _achievements[0];
        collectableAchievement50 = _achievements[1];
        collectableAchievement100 = _achievements[2];
        deathAchievement50 = _achievements[3];
        borderAchievement = _achievements[4];
        deathAchievement1 = _achievements[5];
    }
}