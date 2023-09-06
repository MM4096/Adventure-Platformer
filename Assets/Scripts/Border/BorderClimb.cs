using System;
using UnityEngine;

public class BorderClimb : MonoBehaviour
{
    private void Update()
    {
        transform.position = new Vector2(transform.position.x, GameObject.Find("Player").transform.position.y);
    }
}