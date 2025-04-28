using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metric_WinCountPresenter : IMetric_WinCount
{ 
    private readonly Metric_WinCountModel _model;

    public Metric_WinCountPresenter(Metric_WinCountModel model)
    {
        _model = model;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    #region Inpit

    public void Win()
    {
        _model.Win();
    }

    public void Reset()
    {
        _model.Reset();
    }

    #endregion
}

public interface IMetric_WinCount
{
    void Win();
    void Reset();
}
