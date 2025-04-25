using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerDailyVisualView : View
{
    [SerializeField] private TextMeshProUGUI textTimer;

    public void Tick(string time)
    {
        textTimer.text = time;
    }
}
