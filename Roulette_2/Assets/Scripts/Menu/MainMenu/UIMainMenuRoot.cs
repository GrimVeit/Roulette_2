using System;
using UnityEngine;

public class UIMainMenuRoot : UIRoot
{
    [SerializeField] private MainPanel_Menu _mainPanel;
    [SerializeField] private DailyRewardPanel_Menu _dailyRewardPanel;
    [SerializeField] private TasksPanel_Menu _tasksPanel;

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
    }

    public void Activate()
    {
        _dailyRewardPanel.OnClickToBack += HandleClickToBack_DailyReward;
        _tasksPanel.OnClickToBack += HandleClickToBack_Tasks;

        _mainPanel.OnClickToDailyReward += HandleClickToDailyReward_Main;
        _mainPanel.OnClickToTasks += HandleClickToTasks_Main;

        OpenMainPanel();
    }


    public void Deactivate()
    {
        _dailyRewardPanel.OnClickToBack -= HandleClickToBack_DailyReward;
        _tasksPanel.OnClickToBack -= HandleClickToBack_Tasks;

        _mainPanel.OnClickToDailyReward -= HandleClickToDailyReward_Main;
        _mainPanel.OnClickToTasks -= HandleClickToTasks_Main;

        if (currentPanel != null)
            CloseOtherPanel(currentPanel);
    }

    public void Dispose()
    {
        _mainPanel.Dispose();
        _dailyRewardPanel.Dispose();
        _tasksPanel.Dispose();
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


    #region Input

    #region MainPanel

    public event Action OnClickToDailyReward_Main;
    public event Action OnClickToTasks_Main;

    private void HandleClickToDailyReward_Main()
    {
        OnClickToDailyReward_Main?.Invoke();
    }

    private void HandleClickToTasks_Main()
    {
        OnClickToTasks_Main?.Invoke();
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

    #endregion

}
