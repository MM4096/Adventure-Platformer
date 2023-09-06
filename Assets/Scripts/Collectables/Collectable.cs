using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [HideInInspector] public bool canCollect = true;
    
    public CollectableType type = CollectableType.Normal;

    private Color selfColor;
    // Start is called before the first frame update
    void Start()
    {
        selfColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (canCollect)
        {
            GetComponent<SpriteRenderer>().color = selfColor;
            GetComponent<Rigidbody2D>().simulated = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            GetComponent<Rigidbody2D>().simulated = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            if (PlayerPrefs.GetString("Preferences").Split(',')[2] == "1")
            {
                GetComponent<ParticleSystem>().Play();
                Destroy(gameObject, 0.2f);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public Tuple<CollectableType, Vector2> GetSaveData()
    {
        return Tuple.Create(type, (Vector2)transform.position);
    }
}

public enum CollectableType
{
    Normal,
    Gold
}
