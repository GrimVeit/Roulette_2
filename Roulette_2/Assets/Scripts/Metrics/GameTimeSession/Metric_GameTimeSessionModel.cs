using System;
using System.Collections;
using UnityEngine;

public class Metric_GameTimeSessionModel
{
    private TimeSpan totalGameTime;

    private IEnumerator coroutineTimer;

    private readonly ITimerDailyChangeDay _timerDailyChangeDay;
    private readonly IActivateTaskProvider _taskActivateProvider;

    private readonly string KEY;
    private readonly int MinutesLimit;

    public Metric_GameTimeSessionModel(string key, ITimerDailyChangeDay timerDailyChangeDay, IActivateTaskProvider taskActivateProvider, int minutesLimit)
    {
        KEY = key;
        MinutesLimit = minutesLimit;

        _taskActivateProvider = taskActivateProvider;
        _timerDailyChangeDay = timerDailyChangeDay;

        _timerDailyChangeDay.OnChangeDay += Reset;
    }

    public void Initialize()
    {
        float savedTimesInSeconds = PlayerPrefs.GetFloat(KEY, 0);
        totalGameTime = TimeSpan.FromSeconds(savedTimesInSeconds);

        if(totalGameTime.TotalMinutes >= MinutesLimit)
        {
            _taskActivateProvider.ActivateTask("Spend15Minutes");
        }
        else
        {
            if (coroutineTimer != null)
                Coroutines.Stop(coroutineTimer);

            coroutineTimer = TimerCoroutine();
            Coroutines.Start(coroutineTimer);
        }
    }

    public void Dispose()
    {
        _timerDailyChangeDay.OnChangeDay -= Reset;

        if (coroutineTimer != null)
            Coroutines.Stop(coroutineTimer);

        PlayerPrefs.SetFloat(KEY, (float)totalGameTime.TotalSeconds);
    }

    public void Reset()
    {
        totalGameTime = TimeSpan.Zero;
        PlayerPrefs.SetFloat(KEY, (float)totalGameTime.TotalSeconds);
    }

    private IEnumerator TimerCoroutine()
    {
        while (totalGameTime.TotalMinutes < MinutesLimit)
        {
            totalGameTime = totalGameTime.Add(TimeSpan.FromSeconds(1));

            //Debug.Log($"Time after first activate - " +
            //    $"{totalGameTime.TotalMinutes} minutes, " +
            //    $"{totalGameTime.TotalSeconds} second");

            yield return new WaitForSeconds(1);
        }

        _taskActivateProvider.ActivateTask("Spend15Minutes");
    }
}
