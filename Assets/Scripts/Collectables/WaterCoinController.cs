using System;
using UnityEngine;


public class WaterCoinController : MonoBehaviour
{
    private WaterCoins[] waterCoins;
    public GameObject diamond;

    private void Awake()
    {
        waterCoins = FindObjectsOfType<WaterCoins>();
    }

    private void Start()
    {
        diamond.GetComponent<Collectable>().canCollect = false;
    }

    private void Update()
    {
        foreach (WaterCoins waterCoin in waterCoins)
        {
            if (!waterCoin.isCollected)
            {
                return;
            }
        }

        try
        {
            diamond.GetComponent<Collectable>().canCollect = true;
        }
        catch (Exception e)
        {
            // ignored
        }
    }
}