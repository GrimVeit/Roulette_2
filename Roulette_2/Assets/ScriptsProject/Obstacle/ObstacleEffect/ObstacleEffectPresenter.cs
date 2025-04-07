using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleEffectPresenter
{
    private readonly ObstacleEffectModel _model;

    public ObstacleEffectPresenter(ObstacleEffectModel model)
    {
        _model = model;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    #region Input

    public void AddObstacle(IObstacleEffectProvider obstacleEffectProvider)
    {
        _model.AddObstacleEffectProvider(obstacleEffectProvider);
    }

    public void RemoveObstacle(IObstacleEffectProvider obstacleEffectProvider)
    {
        _model.RemoveObstacleEffectProvider(obstacleEffectProvider);
    }

    public void Clear()
    {
        _model.Clear();
    }

    #endregion
}
