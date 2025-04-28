using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metric_GameTimeSessionPresenter
{
    private readonly Metric_GameTimeSessionModel timeGameSessionModel;

    public Metric_GameTimeSessionPresenter(Metric_GameTimeSessionModel timeGameSessionModel)
    {
        this.timeGameSessionModel = timeGameSessionModel;
    }

    public void Initialize()
    {
        timeGameSessionModel.Initialize();
    }

    public void Dispose()
    {
        timeGameSessionModel.Dispose();
    }
}
