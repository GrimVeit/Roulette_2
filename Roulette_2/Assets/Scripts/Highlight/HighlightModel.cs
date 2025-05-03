using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightModel
{
    public event Action<int> OnSelect;
    public event Action<int> OnDeselect;
    public event Action OnDeselectAll;

    public void Select(int id)
    {
        OnSelect?.Invoke(id);
    }

    public void Deselect(int id)
    {
        OnDeselect?.Invoke(id);
    }

    public void DeselectAll()
    {
        OnDeselectAll?.Invoke();
    }
}
