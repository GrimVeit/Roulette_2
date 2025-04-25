using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipGameVisualModel
{
    public event Action<int, int> OnSpawnChip;

    public void SpawnChips(int id, List<int> indexesPositions)
    {
        for (int i = 0; i < indexesPositions.Count; i++)
        {
            OnSpawnChip?.Invoke(i, indexesPositions[i]);
        }
    }
}
