using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidden : MonoBehaviour
{
    public Gradient gradient;

    private bool isHovering;

    private float hoverTime = 0;

    [HideInInspector] public float[] fadeTime = new float[] { 0.5f, 0.5f };
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = gradient.Evaluate(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isHovering)
        {
            hoverTime += Time.deltaTime / fadeTime[0];
        }
        else
        {
            hoverTime -= Time.deltaTime / fadeTime[1];
        }
        hoverTime = Mathf.Clamp(hoverTime, 0, 1);
        GetComponent<SpriteRenderer>().color = gradient.Evaluate(hoverTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isHovering = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isHovering = false;
            GetComponent<SpriteRenderer>().color = gradient.Evaluate(0);
        }
    }
}
