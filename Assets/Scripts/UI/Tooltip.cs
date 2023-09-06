using System;
using TMPro;
using UnityEngine;


public class Tooltip : MonoBehaviour
{
    public static Tooltip instance;
    [SerializeField] private Canvas canvas;
    private TextMeshProUGUI tooltipText;
    private RectTransform backgroundRectTransform;
    
    private void Awake()
    {
        instance = this;
        backgroundRectTransform = transform.Find("Tooltip").GetComponent<RectTransform>();
        tooltipText = backgroundRectTransform.Find("Text").GetComponent<TextMeshProUGUI>();
        HideTooltip();
    }

    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition, canvas.worldCamera,
            out localPoint);
        localPoint.x += (GetComponent<RectTransform>().rect.width / 2) + 5;
        localPoint.y -= (GetComponent<RectTransform>().rect.height / 2) - 5;
        transform.localPosition = localPoint;
    }

    public void ShowTooltip(string text)
    {
        backgroundRectTransform.gameObject.SetActive(true);
        tooltipText.text = text;
    }
    
    public void HideTooltip()
    {
        backgroundRectTransform.gameObject.SetActive(false);
    }
}