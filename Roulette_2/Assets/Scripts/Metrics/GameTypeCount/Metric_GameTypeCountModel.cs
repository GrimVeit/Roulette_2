using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metric_GameTypeCountModel
{
    private readonly string KEY;
    private readonly List<int> ids = new();
    private readonly int LimitCount;

    private readonly ITimerDailyChangeDay _timerDailyChangeDay;
    private readonly IActivateTaskProvider _taslActivateProvider;

    public Metric_GameTypeCountModel(string key, int limitCount, IActivateTaskProvider activateTaskProvider, ITimerDailyChangeDay timerDailyChangeDay)
    {
        KEY = key;
        LimitCount = limitCount;
        _taslActivateProvider = activateTaskProvider;
        _timerDailyChangeDay = timerDailyChangeDay;

        _timerDailyChangeDay.OnChangeDay += Reset;
    }

    public void Initialize()
    {
        string raw = PlayerPrefs.GetString(KEY, "");

        if(string.IsNullOrEmpty(raw)) return;

        foreach(string s in raw.Split(","))
        {
            if(int.TryParse(s, out int id))
            {
                ids.Add(id);
            }
        }
    }

    public void Dispose()
    {
        _timerDailyChangeDay.OnChangeDay -= Reset;

        if (ids.Count == 0) return;

        string savedData = string.Join(",", ids.ToArray());
        PlayerPrefs.SetString(KEY, savedData);
    }

    private void Reset()
    {
        ids.Clear();
        PlayerPrefs.DeleteKey(KEY);
    }

    public void AddGameType(int id)
    {
        if(ids.Contains(id)) return;

        ids.Add(id);
        Debug.Log("UNIQUE GAME TYPE COUNT: " + ids.Count);

        if(ids.Count == LimitCount)
        {
            _taslActivateProvider.ActivateTask("4DifferentRoulettes");
        }
    }
}
