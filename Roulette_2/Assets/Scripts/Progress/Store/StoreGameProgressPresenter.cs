using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreGameProgressPresenter : IStoreGameProgressEvents
{
    private readonly StoreGameProgressModel _model;

    public StoreGameProgressPresenter(StoreGameProgressModel model)
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

    #region Output

    public event Action<int, bool> OnChangeGameStatus
    {
        add => _model.OnChangeStatusGame += value;
        remove => _model.OnChangeStatusGame -= value;
    }

    #endregion

    #region Input

    public void OpenGame(int id)
    {
        _model.OpenGame(id);
    }

    public void CompleteTutuorial(int id)
    {
        _model.CompleteTutuorial(id);
    }

    public bool HasPlayedTutorialById(int id)
    {
        return _model.HasPlayedTutorialById(id);
    }

    #endregion
}

public interface IStoreGameProgressEvents
{
    public event Action<int, bool> OnChangeGameStatus;
}
