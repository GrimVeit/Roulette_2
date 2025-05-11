using System;
using UnityEngine;

public class UIMainMenuRoot : UIRoot
{
    [SerializeField] private MainPanel_Menu _mainPanel;
    [SerializeField] private DailyRewardPanel_Menu _dailyRewardPanel;
    [SerializeField] private TasksPanel_Menu _tasksPanel;
    [SerializeField] private ChipsPanel_Menu _chipsPanel;

    [Header("Others")]
    [SerializeField] private AvatarNicknamePanel_Menu _avatarNicknamePanel;
    [SerializeField] private SaveAvatarNicknameDataPanel_Menu _saveAvatarNicknameDataPanel;

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

        _avatarNicknamePanel.Initialize();
        _saveAvatarNicknameDataPanel.Initialize();
    }

    public void Activate()
    {
        _saveAvatarNicknameDataPanel.OnClickToSave += HandleClickToSave_AvatarNickname;

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
    }


    public void Deactivate()
    {
        _saveAvatarNicknameDataPanel.OnClickToSave -= HandleClickToSave_AvatarNickname;

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

        _avatarNicknamePanel.Dispose();
        _saveAvatarNicknameDataPanel.Dispose();
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

    #region OTHERS

    public void OpenAvatarNicknamePanel()
    {
        OpenOtherPanel(_avatarNicknamePanel);
    }

    public void CloseAvatarNicknamePanel()
    {
        CloseOtherPanel(_avatarNicknamePanel);
    }



    public void OpenSaveAvatarDataPanel()
    {
        OpenOtherPanel(_saveAvatarNicknameDataPanel);
    }

    public void CloseSaveAvatarDataPanel()
    {
        CloseOtherPanel(_saveAvatarNicknameDataPanel);
    }

    #endregion


    #region Output

    #region OTHER

    public event Action OnClickToSave_AvatarNickname;

    private void HandleClickToSave_AvatarNickname()
    {
        OnClickToSave_AvatarNickname?.Invoke();
    }

    #endregion

    #region MainPanel

    public event Action OnClickToDailyReward_Main;
    public event Action OnClickToTasks_Main;
    public event Action OnClickToChips_Main;

    private void HandleClickToDailyReward_Main()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToDailyReward_Main?.Invoke();
    }

    private void HandleClickToTasks_Main()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToTasks_Main?.Invoke();
    }

    private void HandleClickToChips_Main()
    {
        _soundProvider.PlayOneShot("Click");

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
        _soundProvider.PlayOneShot("Click");

        OnClickToMini?.Invoke();
    }

    private void HandleClickToEuro()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToEuro?.Invoke();
    }

    private void HandleClickToAmerica()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToAmerica?.Invoke();
    }

    private void HandleClickToAmericaMulti()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToAmericaMulti?.Invoke();
    }

    private void HandleClickToFrench()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToFrench?.Invoke();
    }

    private void HandleClickToAmericaTracker()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToAmericaTracker?.Invoke();
    }

    #endregion

    #region DailyRewardPanel

    public event Action OnClickToBack_DailyReward;

    private void HandleClickToBack_DailyReward()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToBack_DailyReward?.Invoke();
    }

    #endregion

    #region TasksPanel

    public event Action OnClickToBack_Tasks;

    private void HandleClickToBack_Tasks()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToBack_Tasks?.Invoke();
    }

    #endregion

    #region ChipsPanel

    public event Action OnClickToBack_Chips;

    private void HandleClickToBack_Chips()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToBack_Chips?.Invoke();
    }

    #endregion

    #endregion

}
