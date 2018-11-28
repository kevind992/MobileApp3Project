using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{

    private static TimerManager instance;

    private float timer;

    private bool startTimer = false;

    // Update is called once per frame
    void Update()
    {
        StartGameTimer();
    }

    public float Timer
    {
        get
        {
            return timer;
        }

        set
        {
            timer = value;
        }
    }

    private void StartGameTimer()
    {
        while (startTimer)
        {
            Debug.Log("Timer Started..");
            timer += Time.deltaTime;
        }
        Debug.Log("Timer Stopped..");
    }

    public bool StartTimer
    {
        get
        {
            return startTimer;
        }

        set
        {
            startTimer = value;
        }
    }

    public static TimerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TimerManager>();
            }

            return instance;
        }
    }
}
