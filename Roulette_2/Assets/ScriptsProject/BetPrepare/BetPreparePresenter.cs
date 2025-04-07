using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetPreparePresenter
{
    private BetPrepareModel _model;
    private BetPrepareView _view;

    public BetPreparePresenter(BetPrepareModel model, BetPrepareView view)
    {
        _model = model;
        _view = view;

        ActivateEvents();
    }

    public void Initialize()
    {
        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnPlay += _model.Play;

        _model.OnActivate += _view.Activate;
        _model.OnDeactivate += _view.Deactivate;
    }

    private void DeactivateEvents()
    {
        _view.OnPlay -= _model.Play;

        _model.OnActivate -= _view.Activate;
        _model.OnDeactivate -= _view.Deactivate;
    }

    #region Input

    public void SetBet(float bet)
    {
        Debug.Log(bet);

        _model.SetBet(bet);
    }

    public void Activate()
    {
        _model.Activate();
    }

    public void Deactivate()
    {
        _model.Deactivate();
    }

    #endregion

    #region Output

    public event Action OnPlay
    {
        add => _model.OnPlay += value;
        remove => _model.OnPlay -= value;
    }

    #endregion
}
