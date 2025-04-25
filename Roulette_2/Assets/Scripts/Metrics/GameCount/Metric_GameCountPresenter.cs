using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metric_GameCountPresenter : IMetric_GameCount
{
    private readonly Metric_GameCountModel _model;

    public Metric_GameCountPresenter(Metric_GameCountModel model)
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

    public void AddGame()
    {
        _model.AddGame();
    }
}

public interface IMetric_GameCount
{
    void AddGame();
}
