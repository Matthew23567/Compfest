using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChange;
    public static Action OnHourChange;

    public static int Minute { get; private set; }
    public static int Hour { get; private set; }

    private float minuteToReal = 0.5f;
    private float timer;

    private void Start()
    {
        Minute = 0;
        Hour = 6;
        timer = minuteToReal;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Minute++;
            OnMinuteChange?.Invoke();
            if (Minute >= 60)
            {
                Hour++;
                Minute = 0;OnHourChange?.Invoke();
            }
            timer = minuteToReal;
        }
    }
}
