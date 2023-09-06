using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Reveal : MonoBehaviour
{
    [SerializeField] private GameObject hidden;
    // Start is called before the first frame update
    void Start()
    {
        hidden.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            hidden.SetActive(false);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Hide());
        }
    }
    
    private IEnumerator Hide()
    {
        yield return new WaitForSeconds(2f);
        hidden.SetActive(true);
    }
}
