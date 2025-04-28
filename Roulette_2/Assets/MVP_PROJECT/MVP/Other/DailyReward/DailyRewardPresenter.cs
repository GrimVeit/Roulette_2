using System;

public class DailyRewardPresenter
{
    private DailyRewardModel _model;
    private DailyRewardView _view;

    public DailyRewardPresenter(DailyRewardModel model, DailyRewardView view)
    {
        _model = model;
        _view = view;

        ActivateEvents();
    }

    public void Initialize()
    {
        _model.Initialize();
        _view.Initialize();

    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnClickDailyReward += _model.GetDailyReward;

        _model.OnActivateButtonReward += _view.ActivateDailyRewardButton;
        _model.OnDeactivateButtonReward += _view.DeactivateDailyRewardButton;
    }

    private void DeactivateEvents()
    {
        _view.OnClickDailyReward -= _model.GetDailyReward;

        _model.OnActivateButtonReward -= _view.ActivateDailyRewardButton;
        _model.OnDeactivateButtonReward -= _view.DeactivateDailyRewardButton;
    }

    #region Input

    public void ActivateButtonReward()
    {
        _model.ActivateButtonReward();
    }

    public void DeactivateButtonReward()
    {
        _model.DeactivateButtonReward();
    }

    public void ResetDailyReward()
    {
        _model.ResetDailyReward();
    }

    #endregion

    #region Output

    public event Action OnGetDailyReward
    {
        add => _model.OnGetDailyReward += value;
        remove => _model.OnGetDailyReward -= value;
    }

    public event Action<int> OnChangeDay
    {
        add => _model.OnChangeCurrentDay += value;
        remove => _model.OnChangeCurrentDay -= value;
    }

    public event Action<int> OnLastOpenDay
    {
        add => _model.OnLastOpenDay += value;
        remove => _model.OnLastOpenDay -= value;
    }

    public event Action OnResetDays
    {
        add => _model.OnResetDays += value;
        remove => _model.OnResetDays -= value;
    }

    #endregion
}
