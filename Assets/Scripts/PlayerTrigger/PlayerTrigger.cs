using System;
using UnityEngine;


public class PlayerTrigger : MonoBehaviour
{
    [Header("On Player Enter")] 
    [SerializeField] private GameObject[] showOnPlayerEnter;
    [SerializeField] private GameObject[] hideOnPlayerEnter;

    [Space(10)] [Header("On Player Exit")] 
    [SerializeField] private GameObject[] showOnPlayerExit;
    [SerializeField] private GameObject[] hideOnPlayerExit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (GameObject g in showOnPlayerEnter)
            {
                g.SetActive(true);
            }
            foreach (GameObject g in hideOnPlayerEnter)
            {
                g.SetActive(false);
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (GameObject g in showOnPlayerExit)
            {
                g.SetActive(true);
            }
            foreach (GameObject g in hideOnPlayerExit)
            {
                g.SetActive(false);
            }
        }
    }
}