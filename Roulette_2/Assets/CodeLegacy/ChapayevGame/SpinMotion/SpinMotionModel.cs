using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMotionModel
{
    public event Action OnBotMotion;
    public event Action OnPlayerMotion;

    public event Action OnStartSpin;

    public void EndSpin(bool isPlayer)
    {
        if (isPlayer)
        {
            OnPlayerMotion?.Invoke();
        }
        else
        {
            OnBotMotion?.Invoke();
        }
    }

    public void StartSpin()
    {
        OnStartSpin?.Invoke();
    }
}
