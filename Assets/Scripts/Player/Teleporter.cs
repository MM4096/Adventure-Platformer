using UnityEngine;


public class Teleporter : MonoBehaviour
{
    public Transform back;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = new Vector2(back.position.x, back.position.y);
        }
    }
}