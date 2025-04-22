using System;
using System.Collections;
using UnityEngine;

public class TimeGameSessionModel
{
    private const string launchKey = "LAUNCH_KEY";

    private TimeSpan totalGameTime;
    private DateTime sessionStartTime;

    private IEnumerator coroutineTimer;

    //public IGameUnlocker gameUnlocker;

    private DateTime startSessionTime;

    //public TimeGameSessionModel(IGameUnlocker gameUnlocker)
    //{
    //    this.gameUnlocker = gameUnlocker;
    //}

    public void Initialize()
    {
        float savedTimesInSeconds = PlayerPrefs.GetFloat(launchKey, 0);
        totalGameTime = TimeSpan.FromSeconds(savedTimesInSeconds);

        if(totalGameTime.TotalMinutes >= 15)
        {
            //gameUnlocker.UnlockGame(GameType.Roulette, 3);
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
        if (coroutineTimer != null)
            Coroutines.Stop(coroutineTimer);

        PlayerPrefs.SetFloat(launchKey, (float)totalGameTime.TotalSeconds);
    }

    private IEnumerator TimerCoroutine()
    {
        while (totalGameTime.TotalMinutes < 15)
        {
            totalGameTime = totalGameTime.Add(TimeSpan.FromSeconds(1));

            Debug.Log($"Time after first activate - " +
                $"{totalGameTime.TotalMinutes} minutes, " +
                $"{totalGameTime.TotalSeconds} second");

            yield return new WaitForSeconds(1);
        }

        //gameUnlocker.UnlockGame(GameType.Roulette, 3);
    }
}
