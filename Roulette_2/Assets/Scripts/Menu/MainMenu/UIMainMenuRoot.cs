using System;
using UnityEngine;

public class UIMainMenuRoot : UIRoot
{
    [SerializeField] private MainPanel_Menu _mainPanel;
    [SerializeField] private DailyRewardPanel_Menu _dailyRewardPanel;
    [SerializeField] private TasksPanel_Menu _tasksPanel;
    [SerializeField] private ChipsPanel_Menu _chipsPanel;

    private ISoundProvider _soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this._soundProvider = soundProvider;
    }

    public void Initialize()
    {
        _mainPanel.Initialize();
        _dailyRewardPanel.Initialize();
        _tasksPanel.Initialize();
        _chipsPanel.Initialize();
    }

    public void Activate()
    {
        _dailyRewardPanel.OnClickToBack += HandleClickToBack_DailyReward;
        _tasksPanel.OnClickToBack += HandleClickToBack_Tasks;
        _chipsPanel.OnClickToBack += HandleClickToBack_Chips;

        _mainPanel.OnClickToDailyReward += HandleClickToDailyReward_Main;
        _mainPanel.OnClickToTasks += HandleClickToTasks_Main;
        _mainPanel.OnClickToChips += HandleClickToChips_Main;

        _mainPanel.OnClickToMini += HandleClickToMini;
        _mainPanel.OnClickToEuro += HandleClickToEuro;
        _mainPanel.OnClickToAmerica += HandleClickToAmerica;
        _mainPanel.OnClickToAmericaMulti += HandleClickToAmericaMulti;
        _mainPanel.OnClickToFrench += HandleClickToFrench;
        _mainPanel.OnClickToAmericaTracker += HandleClickToAmericaTracker;

        OpenMainPanel();
    }


    public void Deactivate()
    {
        _dailyRewardPanel.OnClickToBack -= HandleClickToBack_DailyReward;
        _tasksPanel.OnClickToBack -= HandleClickToBack_Tasks;
        _chipsPanel.OnClickToBack -= HandleClickToBack_Chips;

        _mainPanel.OnClickToDailyReward -= HandleClickToDailyReward_Main;
        _mainPanel.OnClickToTasks -= HandleClickToTasks_Main;
        _mainPanel.OnClickToChips -= HandleClickToChips_Main;

        _mainPanel.OnClickToMini -= HandleClickToMini;
        _mainPanel.OnClickToEuro -= HandleClickToEuro;
        _mainPanel.OnClickToAmerica -= HandleClickToAmerica;
        _mainPanel.OnClickToAmericaMulti -= HandleClickToAmericaMulti;
        _mainPanel.OnClickToFrench -= HandleClickToFrench;
        _mainPanel.OnClickToAmericaTracker -= HandleClickToAmericaTracker;

        if (currentPanel != null)
            CloseOtherPanel(currentPanel);
    }

    public void Dispose()
    {
        _mainPanel.Dispose();
        _dailyRewardPanel.Dispose();
        _tasksPanel.Dispose();
        _chipsPanel.Dispose();
    }


    public void OpenMainPanel()
    {
        OpenPanel(_mainPanel);
    }

    public void OpenDailyRewardPanel()
    {
        OpenPanel(_dailyRewardPanel);
    }

    public void OpenTasksPanel()
    {
        OpenPanel(_tasksPanel);
    }

    public void OpenChipsPanel()
    {
        OpenPanel(_chipsPanel);
    }


    #region Output

    #region MainPanel

    public event Action OnClickToDailyReward_Main;
    public event Action OnClickToTasks_Main;
    public event Action OnClickToChips_Main;

    private void HandleClickToDailyReward_Main()
    {
        OnClickToDailyReward_Main?.Invoke();
    }

    private void HandleClickToTasks_Main()
    {
        OnClickToTasks_Main?.Invoke();
    }

    private void HandleClickToChips_Main()
    {
        OnClickToChips_Main?.Invoke();
    }






    public event Action OnClickToMini;
    public event Action OnClickToEuro;
    public event Action OnClickToAmerica;
    public event Action OnClickToAmericaMulti;
    public event Action OnClickToFrench;
    public event Action OnClickToAmericaTracker;

    private void HandleClickToMini()
    {
        OnClickToMini?.Invoke();
    }

    private void HandleClickToEuro()
    {
        OnClickToEuro?.Invoke();
    }

    private void HandleClickToAmerica()
    {
        OnClickToAmerica?.Invoke();
    }

    private void HandleClickToAmericaMulti()
    {
        OnClickToAmericaMulti?.Invoke();
    }

    private void HandleClickToFrench()
    {
        OnClickToFrench?.Invoke();
    }

    private void HandleClickToAmericaTracker()
    {
        OnClickToAmericaTracker?.Invoke();
    }

    #endregion

    #region DailyRewardPanel

    public event Action OnClickToBack_DailyReward;

    private void HandleClickToBack_DailyReward()
    {
        OnClickToBack_DailyReward?.Invoke();
    }

    #endregion

    #region TasksPanel

    public event Action OnClickToBack_Tasks;

    private void HandleClickToBack_Tasks()
    {
        OnClickToBack_Tasks?.Invoke();
    }

    #endregion

    #region ChipsPanel

    public event Action OnClickToBack_Chips;

    private void HandleClickToBack_Chips()
    {
        OnClickToBack_Chips?.Invoke();
    }

    #endregion

    #endregion

}
