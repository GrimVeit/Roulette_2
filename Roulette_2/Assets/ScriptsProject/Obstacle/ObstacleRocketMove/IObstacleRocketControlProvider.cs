using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObstacleRocketControlProvider
{
    public event Action<ObstacleType, PathZone, IObstacleKnockProvider> OnApplyObstacleRocketControl;
}

public interface IObstacleKnockProvider
{
    public void KnockLeft();
    public void KnockRight();
}
