using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreBetPresenter
{
    private readonly StoreBetModel _model;

    public StoreBetPresenter(StoreBetModel model)
    {
        _model = model;
    }

    public void Initialize()
    {
        _model.Initialize();
    }

    public void Dispose()
    {
        _model.Dispose();
    }

    #region Input

    public void IncreaseBet()
    {
        _model.IncreaseBet();
    }

    public void DecreaseBet()
    {
        _model.DecreaseBet();
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

    public event Action<float> OnChooseBet
    {
        add => _model.OnChooseBet += value;
        remove => _model.OnChooseBet -= value;
    }

    #endregion
}
