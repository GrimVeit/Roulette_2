using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_Menu : MovePanel
{
    [SerializeField] private Button buttonDailyReward;
    [SerializeField] private Button buttonTasks;

    public override void Initialize()
    {
        base.Initialize();

        buttonDailyReward.onClick.AddListener(() => OnClickToDailyReward?.Invoke());
        buttonTasks.onClick.AddListener(() => OnClickToTasks?.Invoke());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonDailyReward.onClick.RemoveListener(() => OnClickToDailyReward?.Invoke());
        buttonTasks.onClick.RemoveListener(() => OnClickToTasks?.Invoke());
    }

    #region Output

    public event Action OnClickToDailyReward;
    public event Action OnClickToTasks;

    #endregion
}
