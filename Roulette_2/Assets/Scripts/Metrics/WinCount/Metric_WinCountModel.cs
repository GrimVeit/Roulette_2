using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metric_WinCountModel
{
    private readonly string KEY;

    private int winRowCount;
    private readonly int WinCountLimit;

    private readonly ITimerDailyChangeDay _timerDailyChangeDay;
    private readonly IActivateTaskProvider _activateTaskProvider;

    public Metric_WinCountModel(string key, int winCountLimit, ITimerDailyChangeDay timerDailyChangeDay, IActivateTaskProvider activateTaskProvider)
    {
        KEY = key;
        WinCountLimit = winCountLimit;
        _timerDailyChangeDay = timerDailyChangeDay;
        _activateTaskProvider = activateTaskProvider;

        _timerDailyChangeDay.OnChangeDay += Reset;
    }

    public void Initialize()
    {
        winRowCount = PlayerPrefs.GetInt(KEY, 0);
    }

    public void Win()
    {
        winRowCount += 1;

        Debug.Log("WIN COUNT SERIES: " + winRowCount);

        if(winRowCount >= WinCountLimit)
        {
            _activateTaskProvider.ActivateTask("Win3TimesRow");
        }
    }

    public void Reset()
    {
        winRowCount = 0;
        PlayerPrefs.SetInt(KEY, 0);
    }

    public void Dispose()
    {
        _timerDailyChangeDay.OnChangeDay -= Reset;

        PlayerPrefs.SetInt(KEY, 0);
    }
}
