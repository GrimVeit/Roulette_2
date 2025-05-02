using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressVisualModel
{
    public event Action<int, bool> OnChangeGameStatus;

    private readonly IStoreGameProgressEvents _gameProgressEvents;

    public GameProgressVisualModel(IStoreGameProgressEvents gameProgressEvents)
    {
        _gameProgressEvents = gameProgressEvents;

        _gameProgressEvents.OnChangeGameStatus += ChangeGameStatus;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _gameProgressEvents.OnChangeGameStatus -= ChangeGameStatus;
    }

    private void ChangeGameStatus(int id, bool status)
    {
        OnChangeGameStatus?.Invoke(id, status);
    }
}
