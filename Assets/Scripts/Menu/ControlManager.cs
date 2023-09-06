using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ControlManager : MonoBehaviour
{
    private bool isChanging;
    private string direction;
    private Button selectedButton;
    [SerializeField] private Button[] buttons;
    
    [HideInInspector] public KeyCode up;
    [HideInInspector] public KeyCode down;
    [HideInInspector] public KeyCode left;
    [HideInInspector] public KeyCode right;
    [HideInInspector] public KeyCode dash;
    [HideInInspector] public KeyCode debug;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Controls"))
        {
            PlayerPrefs.SetString("Controls", "W,S,A,D,Space,C");
        }
        string[] controls = PlayerPrefs.GetString("Controls").Split(',');
        up = (KeyCode)Enum.Parse(typeof(KeyCode), controls[0]);
        down = (KeyCode)Enum.Parse(typeof(KeyCode), controls[1]);
        left = (KeyCode)Enum.Parse(typeof(KeyCode), controls[2]);
        right = (KeyCode)Enum.Parse(typeof(KeyCode), controls[3]);
        dash = (KeyCode)Enum.Parse(typeof(KeyCode), controls[4]);
        debug = (KeyCode)Enum.Parse(typeof(KeyCode), controls[5]);
        buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = controls[0];
        buttons[1].GetComponentInChildren<TextMeshProUGUI>().text = controls[1];
        buttons[2].GetComponentInChildren<TextMeshProUGUI>().text = controls[2];
        buttons[3].GetComponentInChildren<TextMeshProUGUI>().text = controls[3];
        buttons[4].GetComponentInChildren<TextMeshProUGUI>().text = controls[4];
        buttons[5].GetComponentInChildren<TextMeshProUGUI>().text = controls[5];
    }

    private void Update()
    {
        
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (isChanging && e.isKey && e.keyCode != KeyCode.Comma)
        {
            isChanging = false;
            switch (direction)
            {
                case "up":
                    up = e.keyCode;
                    selectedButton.GetComponentInChildren<TextMeshProUGUI>().text = e.keyCode.ToString();
                    break;
                case "down":
                    down = e.keyCode;
                    selectedButton.GetComponentInChildren<TextMeshProUGUI>().text = e.keyCode.ToString();
                    break;
                case "left":
                    left = e.keyCode;
                    selectedButton.GetComponentInChildren<TextMeshProUGUI>().text = e.keyCode.ToString();
                    break;
                case "right":
                    right = e.keyCode;
                    selectedButton.GetComponentInChildren<TextMeshProUGUI>().text = e.keyCode.ToString();
                    break;
                case "dash":
                    dash = e.keyCode;
                    selectedButton.GetComponentInChildren<TextMeshProUGUI>().text = e.keyCode.ToString();
                    break;
                case "debug":
                    debug = e.keyCode;
                    selectedButton.GetComponentInChildren<TextMeshProUGUI>().text = e.keyCode.ToString();
                    break;
            }
            print($"Changed {direction} to {e.keyCode.ToString()}");
            PlayerPrefs.SetString("Controls", up + "," + down + "," + left + "," + right + "," + dash + "," + debug);
        }
    }

    public void SetControl(Button button)
    {
        isChanging = true;
        direction = button.name.ToLower();
        selectedButton = button;
        selectedButton.GetComponentInChildren<TextMeshProUGUI>().text = "Press a key";
    }
}