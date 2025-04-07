using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotStoreChipPresenter
{
    private readonly BotStoreChipModel model;

    public BotStoreChipPresenter(BotStoreChipModel model)
    {
        this.model = model;
    }

    public void Initialize()
    {
        model.Initialize();
    }

    public void Dispose()
    {
        model.Dispose();
    }


    #region Input

    public event Action<Chip> OnSelectRandomChip
    {
        add => model.OnSelectChip += value;
        remove => model.OnSelectChip -= value;
    }

    public void SelectRandomChip(Chip chip)
    {
        model.SelectRandomChip();
    }

    public void Activate()
    {
        model.Activate();
    }

    public void Deactivate()
    {
        model.Deactivate();
    }

    #endregion
}
