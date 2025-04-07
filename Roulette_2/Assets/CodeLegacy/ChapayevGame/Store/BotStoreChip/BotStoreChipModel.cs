using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BotStoreChipModel
{
    public event Action<Chip> OnSelectChip;

    private List<Chip> chips = new List<Chip>();

    private bool isActive;

    public BotStoreChipModel(ChipGroup chipGroup)
    {
        for (int i = 0; i < chipGroup.Chips.Count; i++)
        {
            chips.Add(chipGroup.Chips[i]);
        }

        ShuffleChips();
    }

    public void Initialize()
    {
        
    }

    public void Dispose()
    {
        
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }

    public void SelectRandomChip()
    {
        if(!isActive) return;

        var chip = chips[0];
        chips.Remove(chip);

        OnSelectChip?.Invoke(chip);
    }

    private void ShuffleChips()
    {
        System.Random rng = new();

        int n = chips.Count;

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Chip chip = chips[k];
            chips[k] = chips[n];
            chips[n] = chip;
        }


    }
}
