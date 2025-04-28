using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownPresenter
{
    private readonly CooldownModel _model;
    private readonly CooldownView _view;

    public CooldownPresenter(CooldownModel model, CooldownView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        _model.OnCountdownTimer += _view.ChangeTimer;
        _model.OnSetAvailableButton += _view.ActivateButton;
        _model.OnSetUnvailableButton += _view.DeactivateButton;

        _model.Initialize();
    }

    public void Dispose()
    {
        _model.OnCountdownTimer -= _view.ChangeTimer;
        _model.OnSetAvailableButton -= _view.ActivateButton;
        _model.OnSetUnvailableButton -= _view.DeactivateButton;

        _model.Dispose();
    }

    #region Input

    public void ActivateCooldown()
    {
        _model.ActivateCooldown();
    }

    #endregion

    #region Output

    public event Action OnRewardOverDay
    {
        add => _model.OnRewardOverDay += value;
        remove => _model.OnRewardOverDay -= value;
    }

    public event Action OnAvailable
    {
        add { _model.OnSetAvailableButton += value; }
        remove { _model.OnSetAvailableButton -= value; }
    }

    public event Action OnUnvailable
    {
        add { _model.OnSetUnvailableButton += value; }
        remove { _model.OnSetUnvailableButton -= value; }
    }

    #endregion
}
