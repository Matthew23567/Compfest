using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    public TextMeshProUGUI time;

    private void OnEnable()
    {
        TimeManager.OnMinuteChange += UpdateTime;
        TimeManager.OnHourChange += UpdateTime;
    }

    private void OnDisable()
    {
        TimeManager.OnMinuteChange -= UpdateTime;
        TimeManager.OnHourChange -= UpdateTime;
    }

    private void UpdateTime()
    {
        time.text = $"{TimeManager.Hour:00}:{TimeManager.Minute:00}";
    }
}
