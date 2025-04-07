using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRocketMovePresenter
{
    private readonly ObstacleRocketMoveModel _model;

    public ObstacleRocketMovePresenter(ObstacleRocketMoveModel model)
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

    public void AddObstacle(IObstacleRocketControlProvider obstacleRocketControlProvider)
    {
        _model.AddObstacleRocketControlProvider(obstacleRocketControlProvider);
    }

    public void RemoveObstacle(IObstacleRocketControlProvider obstacleRocketControlProvider)
    {
        _model.RemoveObstacleRocketControlProvider(obstacleRocketControlProvider);
    }

    public void Clear()
    {
        _model.Clear();
    }

    #endregion
}
