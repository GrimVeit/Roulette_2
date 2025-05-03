using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPointerModel
{
    public event Action<int> OnMove;
    public event Action OnActivate;
    public event Action OnDeactivate;

    public void Activate()
    {
        OnActivate?.Invoke();
    }

    public void Deactivate()
    {
        OnDeactivate?.Invoke();
    }

    public void Move(int index)
    {
        OnMove?.Invoke(index);
    }
}
