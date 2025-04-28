using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metric_GameTypeCountPresenter : IMetric_GameTypeCount
{
    private readonly Metric_GameTypeCountModel _model;

    public Metric_GameTypeCountPresenter(Metric_GameTypeCountModel model)
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

    #region Input

    public void AddGameType(int id)
    {
        _model.AddGameType(id);
    }

    #endregion
}

public interface IMetric_GameTypeCount
{
    void AddGameType(int id);
}
