using System;
using UnityEngine;


public class Speedrun : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToShow;
    [SerializeField] private GameObject[] objectsToHide;
    [SerializeField] private GameObject[] rewardHard;
    [SerializeField] private GameObject[] rewardEasy;
    [SerializeField] private GameObject trigger;

    [SerializeField] private float easyTimer;
    [SerializeField] private float hardTimer;
    private float countdown;

    private void Update()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
            foreach (GameObject obj in objectsToShow)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in objectsToHide)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in rewardEasy)
            {
                try
                {
                    if (obj.GetComponent<Collectable>())
                    {
                        obj.GetComponent<Collectable>().canCollect = true;
                    }
                    else
                    {
                        obj.SetActive(true);
                    }
                }
                catch (Exception e)
                {
                    // ignored
                }
            }
            if (countdown > easyTimer - hardTimer)
            {
                try
                {
                    foreach (GameObject obj in rewardHard)
                    {
                        if (obj.GetComponent<Collectable>())
                        {
                            obj.GetComponent<Collectable>().canCollect = true;
                        }
                        else
                        {
                            obj.SetActive(true);
                        }
                    }    
                }
                catch (Exception e)
                {
                    // ignored
                }
            }
            else
            {
                foreach (GameObject obj in rewardHard)
                {
                    try
                    {
                        if (obj.GetComponent<Collectable>())
                        {
                            obj.GetComponent<Collectable>().canCollect = false;
                        }
                        else
                        {
                            obj.SetActive(false);
                        }
                    }
                    catch (Exception e)
                    {
                        // ignored
                    }
                }
            }
            trigger.SetActive(false);
        }
        else
        {
            foreach (GameObject obj in objectsToShow)
            {
                obj.SetActive(false);
            }
            
            foreach (GameObject obj in rewardEasy)
            {
                try
                {
                    if (obj.GetComponent<Collectable>())
                    {
                        obj.GetComponent<Collectable>().canCollect = false;
                    }
                    else
                    {
                        obj.SetActive(false);
                    }
                }
                catch (Exception e)
                {
                    // ignored
                }
            }
            foreach (GameObject obj in rewardHard)
            {
                try
                {
                    if (obj.GetComponent<Collectable>())
                    {
                        obj.GetComponent<Collectable>().canCollect = false;
                    }
                    else
                    {
                        obj.SetActive(false);
                    }
                }
                catch (Exception e)
                {
                    // ignored
                }
            }
            trigger.SetActive(true);
        }
    }

    public void StartTimer()
    {
        Countdown.instance.StartCountdown(easyTimer, easyTimer - hardTimer);
        countdown = easyTimer;
    }
}