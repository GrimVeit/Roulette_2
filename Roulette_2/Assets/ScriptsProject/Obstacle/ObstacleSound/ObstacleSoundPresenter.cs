using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSoundPresenter
{
    private readonly ObstacleSoundModel _model;

    public ObstacleSoundPresenter(ObstacleSoundModel model)
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

    public void AddObstacle(IObstacleSoundProvider obstacleSoundProvider)
    {
        _model.AddObstacleSoundProvider(obstacleSoundProvider);
    }

    public void RemoveObstacle(IObstacleSoundProvider obstacleSoundProvider)
    {
        _model.RemoveObstacleSoundProvider(obstacleSoundProvider);
    }

    public void Clear()
    {
        _model.Clear();
    }

    #endregion
}
