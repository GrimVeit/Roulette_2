using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMultiplierPresenter
{
    private readonly ScoreMultiplierModel _model;
    private readonly ScoreMultiplierView _view;

    public ScoreMultiplierPresenter(ScoreMultiplierModel model, ScoreMultiplierView view)
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
        _model.OnChangeScoreMultipliyer += _view.SetScoreMultiplier;
    }

    private void DeactivateEvents()
    {
        _model.OnChangeScoreMultipliyer -= _view.SetScoreMultiplier;
    }

    #region Input

    public void AddObstacle(IScoreMultiplyProvider scoreMultiplyProvider)
    {
        _model.AddScoreMultiply(scoreMultiplyProvider);
    }

    public void RemoveObstacle(IScoreMultiplyProvider scoreMultiplyProvider)
    {
        _model.RemoveScoreMultiply(scoreMultiplyProvider);
    }

    public void Clear()
    {
        _model.Clear();
    }

    #endregion

    #region Output

    public event Action<float> OnChangeScoreMultipliyer
    {
        add => _model.OnChangeScoreMultipliyer += value;
        remove => _model.OnChangeScoreMultipliyer -= value;
    }

    #endregion
}
