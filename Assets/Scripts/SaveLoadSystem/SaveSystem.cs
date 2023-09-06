using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;
    public float saveVersion;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (PlayerPrefs.GetString("mode") == "t")
        {
            Load();
        }
    }

    public void Save()
    {
        List<Tuple<CollectableType, Vector2>> collectables = new List<Tuple<CollectableType, Vector2>>();

        print(FindObjectsOfType<Collectable>().Length);
        foreach (Collectable collectable in FindObjectsOfType<Collectable>())
        {
            Transform t = collectable.GetComponent<Transform>();
            collectables.Add(new Tuple<CollectableType, Vector2>(
                collectable.gameObject.GetComponent<Collectable>().type,
                new Vector2(Mathf.Round(t.position.x), Mathf.Round(t.position.y))));
        }

        Vector2 playerPosition = GameObject.Find("Player").GetComponent<PlayerMovement>().checkpoint;

        List<Vector2> coins = new List<Vector2>();
        foreach (WaterCoins coin in FindObjectsOfType<WaterCoins>())
        {
            coins.Add(coin.gameObject.transform.position);
        }

        Achievement[] achievements = Achievements.instance.achievements;
        float timePlayed = Timer.instance.GetTime();

        Save save = new Save(playerPosition, collectables.ToArray(), coins.ToArray(), achievements, timePlayed);

        string json = JsonUtility.ToJson(save);

        PlayerPrefs.SetString("save", json + "<SEPARATOR>" + saveVersion);
    }

    public void Load()
    {
        if (!PlayerPrefs.HasKey("save"))
            return;
        if (!PlayerPrefs.GetString("save").Contains("<SEPARATOR>" + saveVersion))
        {
            PlayerPrefs.DeleteKey("save");
            return;
        }

        print(PlayerPrefs.GetString("save"));
        Save save = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("save").Split("<SEPARATOR>")[0]);

        Vector2 playerPosition = new Vector2(save.playerPosition[0], save.playerPosition[1]);

        List<Tuple<CollectableType, Vector2>> collectables = new List<Tuple<CollectableType, Vector2>>();

        print($"{save.collectables.Length} loaded collectables");
        foreach (SaveString collectable in save.collectables)
        {
            collectables.Add(new Tuple<CollectableType, Vector2>(
                (CollectableType)Enum.Parse(typeof(CollectableType), collectable.data[0]),
                new Vector2(float.Parse(collectable.data[1].Split(',')[0]),
                    float.Parse(collectable.data[1].Split(',')[1]))));
        }


        Vector2[] coins = new Vector2[save.coins.Length];

        foreach (SaveFloat coin in save.coins)
        {
            coins[Array.IndexOf(save.coins, coin)] = new Vector2(coin.data[0], coin.data[1]);
        }

        Achievement[] achievements = save.achievements;
        float timePlayed = save.timePlayed;


        GameObject.Find("Player").transform.position = playerPosition;

        List<Collectable> notCollected = new List<Collectable>();
        foreach (var c in collectables)
        {
            foreach (var d in FindObjectsOfType<Collectable>())
            {
                Vector2 modified =
                    new Vector2(Mathf.Round(d.transform.position.x), Mathf.Round(d.transform.position.y));
                if (c.Item2.Equals(modified))
                {
                    notCollected.Add(d);
                    print($"added {c.Item2} to not collected");
                }
            }
        }

        foreach (var c in FindObjectsOfType<Collectable>())
        {
            if (!notCollected.Contains(c))
            {
                print($"destroyed {c.transform.position}");
                Destroy(c.gameObject);
            }
        }

        List<WaterCoins> notCollectedCoins = new List<WaterCoins>();
        foreach (var c in coins)
        {
            foreach (var d in FindObjectsOfType<WaterCoins>())
            {
                if (d.gameObject.transform.position.Equals(c))
                {
                    notCollectedCoins.Add(d);
                }
            }
        }

        foreach (var c in FindObjectsOfType<WaterCoins>())
        {
            if (!notCollectedCoins.Contains(c))
            {
                Destroy(c.gameObject);
            }
        }


        Achievements.instance.UnpackAchievements(achievements);
        Timer.instance.timeElapsed = timePlayed;
    }
}