using System;
using System.Collections.Generic;
using UnityEngine;

public class ChipSpawnerModel
{
    public event Action<ChipMove> OnDestroyChip;

    public event Action<int, Chip> OnChipSpawner;

    private int currentCountChip = 0;
    private List<int> indexPositions = new List<int>();

    private bool isActive = false;

    private ISoundProvider soundProvider;

    public ChipSpawnerModel(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void SetStrategy(Strategy strategy)
    {
        if(!isActive) return;

        indexPositions = strategy.IndexPositions;
    }

    public void SetChip(Chip chip)
    {
        if(!isActive) return;

        Debug.Log(indexPositions.Count);
        Debug.Log(currentCountChip);

        OnChipSpawner?.Invoke(indexPositions[currentCountChip], chip);

        currentCountChip += 1;
    }

    public void DestroyChip(ChipMove chipMove)
    {
        soundProvider.PlayOneShot("Chip_Destroy");

        OnDestroyChip?.Invoke(chipMove);
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }
}
