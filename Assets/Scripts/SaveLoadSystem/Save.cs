using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[Serializable]
public class Save
{
    public float[] playerPosition;
    public SaveString[] collectables;
    public SaveFloat[] coins;
    public Achievement[] achievements;
    public float timePlayed;
    
    public Save(Vector2 playerPosition, Tuple<CollectableType, Vector2>[] collectables, Vector2[] coins, Achievement[] achievements, float timePlayed)
    {
        List<SaveString> conversionCollectables = new List<SaveString>();
        for (int i = 0; i < collectables.Length; i++)
        {
            string[] newString = new string[2];
            newString[0] = collectables[i].Item1.ToString();
            newString[1] = collectables[i].Item2.x + "," + collectables[i].Item2.y;
            conversionCollectables.Add(new SaveString(newString));
        }
        List<SaveFloat> conversionCoins = new List<SaveFloat>();
        for (int i = 0; i < coins.Length; i++)
        {
            conversionCoins.Add(new SaveFloat(new float[] {coins[i].x, coins[i].y}));
        }
        this.playerPosition = new []{playerPosition.x, playerPosition.y};
        this.collectables = conversionCollectables.ToArray();
        this.coins = conversionCoins.ToArray();
        this.achievements = achievements;
        this.timePlayed = timePlayed;
    }
}

[Serializable]
public class SaveString
{
    public string[] data;

    public SaveString(string[] saves)
    {
        data = saves;
    }
}

[Serializable]
public class SaveFloat
{
    public float[] data;

    public SaveFloat(float[] saves)
    {
        data = saves;
    }
}