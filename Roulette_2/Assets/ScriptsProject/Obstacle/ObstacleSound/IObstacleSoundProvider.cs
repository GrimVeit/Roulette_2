using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObstacleSoundProvider
{
    public event Action<string> OnApplyObstacleSound;
}
