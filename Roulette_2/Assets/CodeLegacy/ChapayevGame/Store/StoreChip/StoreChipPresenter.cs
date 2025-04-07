using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreChipPresenter : IStoreChipData
{
    private readonly StoreChipModel model;

    public StoreChipPresenter(StoreChipModel model)
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

    public event Action<Chip> OnOpenNewChip
    {
        add => model.OnOpenNewChip += value;
        remove => model.OnOpenNewChip -= value;
    }

    public event Action<Chip> OnOpenChip
    {
        add => model.OnOpenChip += value;
        remove => model.OnOpenChip -= value;
    }

    public event Action<Chip> OnCloseChip
    {
        add => model.OnCloseChip += value;
        remove => model.OnCloseChip -= value;
    }

    public event Action<Chip> OnDeselectChip
    {
        add => model.OnDeselectChip += value;
        remove => model.OnDeselectChip -= value;
    }

    public event Action<Chip> OnSelectChip
    {
        add => model.OnSelectChip += value;
        remove => model.OnSelectChip -= value;
    }

    public bool IsAvailableChip()
    {
        return model.IsAvailableChip();
    }

    public Chip GetRandomCloseChip()
    {
        return model.GetRandomCloseChip();
    }


    public void SelectChip(int id)
    {
        model.SelectChip(id);
    }

    public void UnselectAllChips()
    {
        model.UnselectAllChips();
    }

    public void OpenChip(int id)
    {
        model.OpenChip(id);
    }

    #endregion
}

public interface IStoreChipData
{
    public bool IsAvailableChip();

    public Chip GetRandomCloseChip();
}
