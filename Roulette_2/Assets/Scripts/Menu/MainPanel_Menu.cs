using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_Menu : MovePanel
{
    [SerializeField] private Button buttonDailyReward;
    [SerializeField] private Button buttonLeaderboard;
    [SerializeField] private Button buttonTasks;
    [SerializeField] private Button buttonChips;

    [SerializeField] private Button buttonMini;
    [SerializeField] private Button buttonEuro;
    [SerializeField] private Button buttonAmerica;
    [SerializeField] private Button buttonAmericaMulti;
    [SerializeField] private Button buttonFrench;
    [SerializeField] private Button buttonAmericaTracker;

    public override void Initialize()
    {
        base.Initialize();

        buttonDailyReward.onClick.AddListener(() => OnClickToDailyReward?.Invoke());
        buttonLeaderboard.onClick.AddListener(() => OnClickToLeaderboard?.Invoke());
        buttonTasks.onClick.AddListener(() => OnClickToTasks?.Invoke());
        buttonChips.onClick.AddListener(() => OnClickToChips?.Invoke());

        buttonMini.onClick.AddListener(() => OnClickToMini?.Invoke());
        buttonEuro.onClick.AddListener(() => OnClickToEuro?.Invoke());
        buttonAmerica.onClick.AddListener(() => OnClickToAmerica?.Invoke());
        buttonAmericaMulti.onClick.AddListener(() => OnClickToAmericaMulti?.Invoke());
        buttonFrench.onClick.AddListener(() => OnClickToFrench?.Invoke());
        buttonAmericaTracker.onClick.AddListener(() => OnClickToAmericaTracker?.Invoke());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonDailyReward.onClick.RemoveListener(() => OnClickToDailyReward?.Invoke());
        buttonLeaderboard.onClick.RemoveListener(() => OnClickToLeaderboard?.Invoke());
        buttonTasks.onClick.RemoveListener(() => OnClickToTasks?.Invoke());
        buttonChips.onClick.RemoveListener(() => OnClickToChips?.Invoke());

        buttonMini.onClick.RemoveListener(() => OnClickToMini?.Invoke());
        buttonEuro.onClick.RemoveListener(() => OnClickToEuro?.Invoke());
        buttonAmerica.onClick.RemoveListener(() => OnClickToAmerica?.Invoke());
        buttonAmericaMulti.onClick.RemoveListener(() => OnClickToAmericaMulti?.Invoke());
        buttonFrench.onClick.RemoveListener(() => OnClickToFrench?.Invoke());
        buttonAmericaTracker.onClick.RemoveListener(() => OnClickToAmericaTracker?.Invoke());
    }

    #region Output

    public event Action OnClickToDailyReward;
    public event Action OnClickToLeaderboard;
    public event Action OnClickToTasks;
    public event Action OnClickToChips;

    public event Action OnClickToMini;
    public event Action OnClickToEuro;
    public event Action OnClickToAmerica;
    public event Action OnClickToAmericaMulti;
    public event Action OnClickToFrench;
    public event Action OnClickToAmericaTracker;

    #endregion
}
