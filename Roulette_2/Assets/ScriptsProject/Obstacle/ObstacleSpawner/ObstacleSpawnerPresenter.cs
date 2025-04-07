using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerPresenter
{
    private readonly ObstacleSpawnerModel _model;
    private readonly ObstacleSpawnerView _view;

    public ObstacleSpawnerPresenter(ObstacleSpawnerModel model, ObstacleSpawnerView view)
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
        _model.OnSpawnObstacle += _view.SpawnObstacle;
    }

    private void DeactivateEvents()
    {
        _model.OnSpawnObstacle -= _view.SpawnObstacle;
    }

    #region Input

    public void ActivateSpawner()
    {
        _model.ActivateSpawner();
    }

    public void DeactivateSpawner()
    {
        _model.DeactivateSpawner();
    }

    #endregion

    #region Output

    public event Action<Obstacle> OnSpawnObstacle
    {
        add => _view.OnSpawnObstacle += value;
        remove => _view.OnSpawnObstacle -= value;
    }

    #endregion
}
