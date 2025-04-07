using System;
using UnityEngine;

public interface IObstacleEffectProvider
{
    public event Action<string, Transform> OnApplyObstacleEffect;
}
