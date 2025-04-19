
using System;
using UnityEngine;

public class RouletteBallPresenter
{
    private readonly RouletteBallModel _model;
    private readonly RouletteBallView _view;

    public RouletteBallPresenter(RouletteBallModel model, RouletteBallView view)
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
        _view.OnBallStopped += _model.BallStopped;

        _model.OnStartSpin += _view.StartSpin;
    }

    private void DeactivateEvents()
    {
        _view.OnBallStopped -= _model.BallStopped;

        _model.OnStartSpin -= _view.StartSpin;
    }

    #region Input

    public void StartSpin()
    {
        _model.StartSpin();
    }

    #endregion

    #region Output

    public event Action<Vector3> OnBallStopped
    {
        add { _model.OnBallStopped += value; }
        remove { _model.OnBallStopped -= value; }
    }

    #endregion
}
