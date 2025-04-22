using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDailyModel
{
    public event Action<string> OnTimerTick;
    public event Action OnChangeDay;

    private float timeUntilNextDay;

    private DateTime lastExitDate;
    private DateTime currentDate => DateTime.UtcNow;
    private DateTime nextMidnight;
    private readonly string KEY;

    private IEnumerator coroutineTimer;

    public TimerDailyModel(string key)
    {
        KEY = key;
    }

    public void Initialize()
    {
        lastExitDate = LoadLastExitDate();

        if(currentDate > lastExitDate)
        {
            Debug.Log("CHANGE DAY");
            OnChangeDay?.Invoke();
        }

        nextMidnight = currentDate.Date.AddDays(1);
        ActivateTimer();
    }

    private void ActivateTimer()
    {
        if(coroutineTimer != null)
            Coroutines.Stop(coroutineTimer);

        coroutineTimer = CheckDayChange();
        Coroutines.Start(coroutineTimer);
    }

    public void Dispose()
    {
        SaveExitDate();
    }

    private IEnumerator CheckDayChange()
    {
        while (true)
        {
            TimeSpan timeLeft = nextMidnight - currentDate;

            if(timeLeft.TotalSeconds <= 0)
            {
                OnChangeDay?.Invoke();
                nextMidnight = currentDate.Date.AddDays(1);
            }

            string formatted = string.Format("{0:D2}:{1:D2}:{2:D2}", timeLeft.Hours, timeLeft.Minutes, timeLeft.Seconds);
            OnTimerTick?.Invoke(formatted);
            Debug.Log(formatted);

            yield return new WaitForSeconds(1);
        }
    }

    private DateTime LoadLastExitDate()
    {
        var str = PlayerPrefs.GetString(KEY, "");
        if (DateTime.TryParse(str, out DateTime parsedDate))
        {
            return parsedDate;
        }

        return DateTime.UtcNow.Date;
    }

    private void SaveExitDate()
    {
        PlayerPrefs.SetString(KEY, currentDate.Date.ToString("yyyy-MM-dd"));
    }
}
