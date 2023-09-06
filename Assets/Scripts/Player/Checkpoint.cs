using System;
using UnityEngine;


public class Checkpoint : MonoBehaviour
{
    private bool isCheckpoint;

    private void Update()
    {
        isCheckpoint = PlayerMovement.instance.checkpoint == (Vector2)transform.position;
        if (GetComponent<SpriteRenderer>() != null)
        {
            GetComponent<SpriteRenderer>().color = isCheckpoint ? Color.green : Color.white;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement.instance.checkpoint = transform.position;
        }
    }
    
}