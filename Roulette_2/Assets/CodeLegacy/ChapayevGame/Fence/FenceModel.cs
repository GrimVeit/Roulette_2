using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceModel
{
    public event Action OnRandomFence;

    public void RandomFence()
    {
        OnRandomFence?.Invoke();
    }
}
