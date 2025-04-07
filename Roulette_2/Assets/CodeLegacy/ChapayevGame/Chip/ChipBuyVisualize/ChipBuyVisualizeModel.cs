using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipBuyVisualizeModel
{
    public event Action<Chip> OnSetOpenChip;
    public event Action<Chip> OnSetNewOpenChip;

    public event Action<Chip> OnSetCloseChip;

    public void SetOpenChip(Chip chip)
    {
        OnSetOpenChip?.Invoke(chip);
    }

    public void SetNewOpenChip(Chip chip)
    {
        OnSetNewOpenChip?.Invoke(chip);
    }

    public void SetCloseChip(Chip chip)
    {
        OnSetCloseChip?.Invoke(chip);
    }
}
