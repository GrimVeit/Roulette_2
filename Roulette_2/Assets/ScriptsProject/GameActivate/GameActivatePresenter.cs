using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActivatePresenter
{
    private readonly GameActivateModel _model;
    private readonly GameActivateView _view;

    public GameActivatePresenter(GameActivateModel model, GameActivateView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        _view.OnPlayGame += _model.PlayGame;

        _model.OnActivate += _view.Activate;
        _model.OnDeactivate += _view.Deactivate;
    }

    private void DeactivateEvents()
    {
        _view.OnPlayGame -= _model.PlayGame;

        _model.OnActivate -= _view.Activate;
        _model.OnDeactivate -= _view.Deactivate;
    }

    #region Input

    public void SetBet(float bet)
    {
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

    public event Action OnPlayGame
    {
        add => _model.OnPlayGame += value;
        remove => _model.OnPlayGame -= value;
    }

    #endregion
}
