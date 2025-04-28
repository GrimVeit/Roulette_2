using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFrameModel
{
    public event Action<string, int> OnActivateAnimation;
    public event Action<string> OnDeactivateAnimation;

    public void ActivateAnimation(string id, int cycles)
    {
        OnActivateAnimation?.Invoke(id, cycles);
    }

    public void DeactivateAnimation(string id)
    {
        OnDeactivateAnimation?.Invoke(id);
    }
}
