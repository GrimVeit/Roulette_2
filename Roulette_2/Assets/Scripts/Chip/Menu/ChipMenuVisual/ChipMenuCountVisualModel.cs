using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipMenuCountVisualModel
{
    public event Action<int, int> OnChangeChipsCount;

    public void ChangeChipsCount(int id, int count)
    {
        OnChangeChipsCount?.Invoke(id, count);
    }
}
