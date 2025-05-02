using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressVisualPresenter
{
    private readonly GameProgressVisualModel _model;
    private readonly GameProgressVisualView _view;

    public GameProgressVisualPresenter(GameProgressVisualModel model, GameProgressVisualView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        _model.Initialize();

        ActivateEvents();
    }

    public void Dispose()
    {
        _model?.Dispose();

        DeacvtivateEvents();
    }

    private void ActivateEvents()
    {
        _model.OnChangeGameStatus += _view.ChangeGameStatus;
    }

    private void DeacvtivateEvents()
    {
        _model.OnChangeGameStatus -= _view.ChangeGameStatus;
    }
}
