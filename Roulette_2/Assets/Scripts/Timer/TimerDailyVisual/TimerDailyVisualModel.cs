using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDailyVisualModel
{
    private readonly ITimerDailyTickable _timerDailyTickable;
    
    public TimerDailyVisualModel(ITimerDailyTickable timerDailyTickable)
    {
        _timerDailyTickable = timerDailyTickable;
    }

    public void Initialize()
    {
        _timerDailyTickable.OnTimerTick += Tickable;
    }

    public void Dispose()
    {
        _timerDailyTickable.OnTimerTick -= Tickable;
    }

    private void Tickable(string time) => OnTickable?.Invoke(time);

    #region Output

    public event Action<string> OnTickable;

    #endregion
}
