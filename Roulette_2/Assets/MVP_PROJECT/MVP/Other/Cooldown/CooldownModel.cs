using System;
using System.Collections;
using UnityEngine;

public class CooldownModel
{
    public event Action OnRewardOverDay;

    public event Action OnAvailable;
    public event Action OnUnvailable;

    public event Action<string> OnCountdownTimer;

    private readonly string KEY;

    private TimeSpan timeToReload;
    private DateTime nextRewardTime;
    private bool isRewardAvailable => DateTime.Now >= nextRewardTime;
    private bool isRewardAvailableOverDay => DateTime.Now >= nextRewardTime + timeToReload;

    private IEnumerator countdownButton_coroutine;

    public CooldownModel(string key, TimeSpan timeToReload)
    {
        KEY = key;
        this.timeToReload = timeToReload;
    }

    public void Initialize()
    {
        nextRewardTime = DateTime.Parse(PlayerPrefs.GetString(KEY, DateTime.Now.ToString()));

        if (isRewardAvailableOverDay)
        {
            OnRewardOverDay?.Invoke();
        }

        Debug.Log(nextRewardTime);

        ActivateCountdown();
    }

    public void Dispose()
    {

    }

    public void ActivateCooldown()
    {
        nextRewardTime = DateTime.Now + timeToReload;
        PlayerPrefs.SetString(KEY, nextRewardTime.ToString());
        ActivateCountdown();

        Debug.Log("Запуск нового таймера");
    }

    private void ActivateCountdown()
    {
        DeactivateCountdown();

        countdownButton_coroutine = Countdown_Coroutine();

        Coroutines.Start(countdownButton_coroutine);
    }

    private void DeactivateCountdown()
    {
        if (countdownButton_coroutine != null)
            Coroutines.Stop(countdownButton_coroutine);
    }

    private IEnumerator Countdown_Coroutine()
    {
        OnUnvailable?.Invoke();

        Debug.Log(isRewardAvailable);

        while (!isRewardAvailable)
        {
            TimeSpan timeRemaining = nextRewardTime - DateTime.Now;

            OnCountdownTimer?.Invoke(string.Format("{0:D2}:{1:D2}:{2:D2}", timeRemaining.Hours, timeRemaining.Minutes, timeRemaining.Seconds));

            if (timeRemaining.TotalSeconds == 0)
            {
                OnAvailable?.Invoke();
                break;
            }

            yield return new WaitForSeconds(1);
        }

        OnAvailable?.Invoke();
    }
}
