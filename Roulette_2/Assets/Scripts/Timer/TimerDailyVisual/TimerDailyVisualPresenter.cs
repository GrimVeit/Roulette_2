using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDailyVisualPresenter
{
    private readonly TimerDailyVisualModel _model;
    private readonly TimerDailyVisualView _view;

    public TimerDailyVisualPresenter(TimerDailyVisualModel model, TimerDailyVisualView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _model.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
    }

    private void ActivateEvents()
    {
        _model.OnTickable += _view.Tick;
    }

    private void DeactivateEvents()
    {
        _model.OnTickable -= _view.Tick;
    }
}
