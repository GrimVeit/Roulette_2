using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePresenter
{
    private readonly ScoreModel _model;
    private readonly ScoreView _view;

    public ScorePresenter(ScoreModel model, ScoreView view)
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
        _model.OnChangeCurrentGame += _view.SetCurrentWinScore;
        _model.OnChangeGameLast += _view.SetLastWinScore;
    }

    private void DeactivateEvents()
    {
        _model.OnChangeCurrentGame -= _view.SetCurrentWinScore;
        _model.OnChangeGameLast -= _view.SetLastWinScore;
    }

    #region Input

    public void SetBet(float bet)
    {
        _model.SetBet(bet);
    }

    public void SetMultiplier(float multiplier)
    {
        _model.SetMultiplier(multiplier);
    }

    public void Win()
    {
        _model.Win();
    }

    #endregion
}
