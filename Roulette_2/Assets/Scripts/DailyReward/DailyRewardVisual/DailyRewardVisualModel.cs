using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewardVisualModel
{
    public event Action OnDeactivateAllDays;
    public event Action<int> OnActivateDay;

    public void ActivateDay(int day)
    {
        OnActivateDay?.Invoke(day);
    }

    public void DeactivateAllDays()
    {
        OnDeactivateAllDays?.Invoke();
    }
}
