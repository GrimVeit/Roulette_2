using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFrameModel
{
    public event Action<string, Transform, int> OnActivateAnimation;

    public void ActivateAnimation(string id, Transform target, int cycles)
    {
        OnActivateAnimation?.Invoke(id, target, cycles);
    }
}
