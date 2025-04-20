using System;
using UnityEngine;

public class RoulettePresenter : IRouletteValueProvider
{
    private readonly RouletteModel _model;
    private readonly RouletteView _view;

    public RoulettePresenter(RouletteModel model, RouletteView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnGetRouletteNumber += _model.GetRouletteNumber;
        _view.OnStop += _model.Stop;

        _model.OnStartSpin += _view.StartSpin;
        _model.OnRollBallToSlot += _view.RollBallToSlot;
    }

    private void DeactivateEvents()
    {
        _view.OnGetRouletteNumber -= _model.GetRouletteNumber;
        _view.OnStop -= _model.Stop;

        _model.OnStartSpin -= _view.StartSpin;
        _model.OnRollBallToSlot -= _view.RollBallToSlot;
    }

    #region Input

    public void StartSpin()
    {
        _model.StartSpin();
    }

    public void RollBallToSlot(Vector3 vector)
    {
        _model.RollBallToSlot(vector);
    }

    #endregion

    #region Output


    public event Action OnStopSpin
    {
        add => _model.OnStopSpin += value;
        remove => _model.OnStopSpin -= value;
    }


    public event Action<RouletteNumber> OnGetRouletteSlotValue
    {
        add { _model.OnGetRouletteSlotValue += value; }
        remove { _model.OnGetRouletteSlotValue -= value; }
    }

    #endregion
}

public interface IRouletteValueProvider
{
    public event Action<RouletteNumber> OnGetRouletteSlotValue;
}
