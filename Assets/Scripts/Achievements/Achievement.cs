using System;
    
[Serializable]
public class Achievement
{
    public string name;
    public string description;
    public AchievementType type;
    public bool unlocked;
    
    public Achievement(string name, string description, AchievementType type, bool unlocked)
    {
        this.name = name;
        this.description = description;
        this.type = type;
        this.unlocked = unlocked;
    }
}

public enum AchievementType
{
    Common,
    Rare,
    Epic,
    Legendary
}