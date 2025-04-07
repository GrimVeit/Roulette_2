using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformModel
{
    public event Action OnActivatePlatform;
    public event Action OnDeactivatePlatform;

    public void ActivatePlatform()
    {
        OnActivatePlatform?.Invoke();
    }

    public void DeactivatePlatform()
    {
        OnDeactivatePlatform?.Invoke();
    }
}
