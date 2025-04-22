using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGameSessionPresenter
{
    private TimeGameSessionModel timeGameSessionModel;

    public TimeGameSessionPresenter(TimeGameSessionModel timeGameSessionModel)
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
