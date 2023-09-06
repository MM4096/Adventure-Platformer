using System;
using UnityEngine;

public class CloudHidden : MonoBehaviour
{
    [SerializeField] private Vector2 topLeft;
    [SerializeField] private Vector2 bottomRight;
    [SerializeField] private Transform player;

    private void Update()
    {
        transform.position = new Vector3(Mathf.Round(Mathf.Clamp(player.position.x, topLeft.x, bottomRight.x)), Mathf.Round(Mathf.Clamp(player.position.y, bottomRight.y, topLeft.y)), 0);
    }
}