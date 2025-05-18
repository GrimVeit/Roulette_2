using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metric_BetNumberModel
{
    private readonly string KEY;

    private int betNumberCount;
    private readonly int BetNumberLimit;

    private readonly ITimerDailyChangeDay _timerDailyChangeDay;
    private readonly IActivateTaskProvider _activateTaskProvider;

    public Metric_BetNumberModel(string key, int winCountLimit, ITimerDailyChangeDay timerDailyChangeDay, IActivateTaskProvider activateTaskProvider)
    {
        KEY = key;
        BetNumberLimit = winCountLimit;
        _timerDailyChangeDay = timerDailyChangeDay;
        _activateTaskProvider = activateTaskProvider;

        _timerDailyChangeDay.OnChangeDay += Reset;
    }

    public void Initialize()
    {
        betNumberCount = PlayerPrefs.GetInt(KEY, 0);
    }

    public void BetNumber()
    {
        betNumberCount += 1;

        Debug.Log("BET NUMBER COUNTS: " + betNumberCount);

        if (betNumberCount >= BetNumberLimit)
        {
            _activateTaskProvider.ActivateTask("BetNumber");
        }
    }

    private void Reset()
    {
        betNumberCount = 0;
        PlayerPrefs.SetInt(KEY, 0);
    }

    public void Dispose()
    {
        _timerDailyChangeDay.OnChangeDay -= Reset;

        PlayerPrefs.SetInt(KEY, 0);
    }
}
