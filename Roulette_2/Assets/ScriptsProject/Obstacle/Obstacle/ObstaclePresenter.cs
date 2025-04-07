using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePresenter
{
    private readonly ObstacleView _view;

    public ObstaclePresenter(ObstacleView view)
    {
        _view = view;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    #region Input

    public void AddObstacle(Obstacle obstacle)
    {
        _view.AddObstacle(obstacle);
    }

    public void StopObstacles()
    {
        _view.StopObstacles();
    }

    public void ClearObstacles()
    {
        _view.ClearObstacles();
    }

    #endregion

    #region Output

    public event Action<Obstacle> OnDestroyObstacle
    {
        add => _view.OnDestroyObstacle += value;
        remove => _view.OnDestroyObstacle -= value;
    }

    #endregion
}
