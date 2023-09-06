using System;
using System.Collections;
using UnityEngine;


public class LavaTrigger : MonoBehaviour
{
    private Transform player;
    [SerializeField] private Animator lava;
    [SerializeField] private Vector2 towerBounds;
    private static readonly int Lava = Animator.StringToHash("Lava");


    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (player.position.x < towerBounds.x || player.position.y < towerBounds.y)
        {
            lava.Play("RisingLava", 0, 0);
            StartCoroutine(ResetAnimation());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            lava.enabled = true;
            lava.Play("RisingLava", 0, 0);
        }
    }

    private IEnumerator ResetAnimation()
    {
        yield return new WaitForEndOfFrame();
        lava.enabled = false;
    } 
}