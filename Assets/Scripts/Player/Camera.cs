using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform player;
    private Vector2 goTo;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        goTo = player.position;
        transform.position = new Vector3(goTo.x, goTo.y, -10);
    }

    private void FixedUpdate()
    {
    }
}
