using UnityEngine;

public class Metric_GameCountModel
{
    private readonly IActivateTaskProvider _activateTaskProvider;
    private readonly ITimerDailyChangeDay _timerDailyChangeDay;

    private readonly string KEY;
    private readonly int limit;
    private int currentGameCount = 0;
    private bool isAlreadyActivated = false;

    public Metric_GameCountModel(string key, IActivateTaskProvider activateTaskProvider, ITimerDailyChangeDay timerDailyChangeDay, int limit)
    {
        KEY = key;
        _activateTaskProvider = activateTaskProvider;
        _timerDailyChangeDay = timerDailyChangeDay;
        this.limit = limit;

        _timerDailyChangeDay.OnChangeDay += Reset;
    }

    public void Initialize()
    {
        currentGameCount = PlayerPrefs.GetInt(KEY, 0);
    }

    public void Reset()
    {
        isAlreadyActivated = false;
        currentGameCount = 0;
        PlayerPrefs.SetInt(KEY, currentGameCount);
    }

    public void AddGame()
    {
        currentGameCount += 1;

        Debug.Log("GAME COUNTS: " + currentGameCount);

        if(currentGameCount >= limit && !isAlreadyActivated)
        {
            isAlreadyActivated = true;
            _activateTaskProvider.ActivateTask("10games");
        }
    }

    public void Dispose()
    {
        _timerDailyChangeDay.OnChangeDay -= Reset;

        PlayerPrefs.SetInt(KEY, currentGameCount);
    }
}
