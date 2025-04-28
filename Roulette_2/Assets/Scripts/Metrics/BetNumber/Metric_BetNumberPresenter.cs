using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metric_BetNumberPresenter : IMetric_BetNumber
{
    private readonly Metric_BetNumberModel _model;

    public Metric_BetNumberPresenter(Metric_BetNumberModel model)
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

    public void BetNumber()
    {
        _model.BetNumber();
    }

    #endregion

}

public interface IMetric_BetNumber
{
    void BetNumber();
}