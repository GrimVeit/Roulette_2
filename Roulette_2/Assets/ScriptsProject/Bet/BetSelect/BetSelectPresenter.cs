using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetSelectPresenter
{
    private readonly BetSelectModel _model;
    private readonly BetSelectView _view;

    public BetSelectPresenter(BetSelectModel model, BetSelectView view)
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
        _view.OnIncrease += _model.IncreaseBet;
        _view.OnDecrease += _model.DecreaseBet;

        _model.OnSetBet += _view.SetBet;
    }

    private void DeactivateEvents()
    {
        _view.OnIncrease -= _model.IncreaseBet;
        _view.OnDecrease -= _model.DecreaseBet;

        _model.OnSetBet -= _view.SetBet;
    }

    #region Input

    public void SetBet(float bet)
    {
        _model.SetBet(bet);
    }

    #endregion

    #region Output

    public event Action OnIncreaseBet
    {
        add => _model.OnIncreaseBet += value;
        remove => _model.OnIncreaseBet -= value;
    }

    public event Action OnDecreaseBet
    {
        add => _model.OnDecreaseBet += value;
        remove => _model.OnDecreaseBet -= value;
    }

    #endregion
}
