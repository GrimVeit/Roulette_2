using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewardVisualPresenter
{
    private readonly DailyRewardVisualModel _model;
    private readonly DailyRewardVisualView _view;

    public DailyRewardVisualPresenter(DailyRewardVisualModel model, DailyRewardVisualView view)
    {
        _model = model;
        _view = view;

        ActivateEvents();
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        _model.OnActivateDay += _view.ActivateDay;
        _model.OnDeactivateAllDays += _view.AllDeactivate;
    }

    private void DeactivateEvents()
    {
        _model.OnActivateDay -= _view.ActivateDay;
        _model.OnDeactivateAllDays -= _view.AllDeactivate;
    }

    #region Input

    public void DeactivateDays()
    {
        _model.DeactivateAllDays();
    }

    public void ActivateDay(int day)
    {
        _model.ActivateDay(day);
    }

    #endregion
}
