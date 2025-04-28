using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewardVisualView : View
{
    [SerializeField] private List<DailyRewardVisual> dailyRewardVisuals = new List<DailyRewardVisual>();

    public void AllDeactivate()
    {
        dailyRewardVisuals.ForEach(drv => drv.Deactivate());
    }

    public void ActivateDay(int day)
    {
        dailyRewardVisuals.ForEach(drv =>
        {
            if(drv.Day <= day)
            {
                drv.Activate();
            }
            else
            {
                drv.Deactivate();
            }
        });
    }
}
