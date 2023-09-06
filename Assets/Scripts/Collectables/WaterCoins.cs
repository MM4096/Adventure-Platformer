using System;
using UnityEngine;


public class WaterCoins : MonoBehaviour
{
    [HideInInspector] public bool isCollected;
    private Color collectedColor = new Color(255, 255, 255, 0);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isCollected = true;
            GetComponent<SpriteRenderer>().color = collectedColor;
        }
    }
}