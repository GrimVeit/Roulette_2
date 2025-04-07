using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipSpawnerPresenter : IChipBank
{
    private readonly ChipSpawnerModel model;
    private readonly ChipSpawnerView view;

    public ChipSpawnerPresenter(ChipSpawnerModel model, ChipSpawnerView view)
    {
        this.model = model;
        this.view = view;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        view.OnDestroyChip += model.DestroyChip;

        model.OnChipSpawner += view.SetChip;
    }

    private void DeactivateEvents()
    {
        view.OnDestroyChip -= model.DestroyChip;

        model.OnChipSpawner -= view.SetChip;
    }

    #region Input

    public event Action<ChipMove> OnSpawnChip
    {
        add => view.OnSpawnChip += value;
        remove => view.OnSpawnChip -= value;
    }

    public event Action<ChipMove> OnDestroyChip
    {
        add => model.OnDestroyChip += value;
        remove => model.OnDestroyChip -= value;
    }

    public event Action<Transform, Transform, Vector2, float> OnPunch
    {
        add => view.OnPunch += value;
        remove => view.OnPunch -= value;
    }

    public void Activate()
    {
        model.Activate();
    }

    public void Deactivate()
    {
        model.Deactivate();
    }

    public void SetStrategy(Strategy strategy)
    {
        model.SetStrategy(strategy);
    }

    public void SetChip(Chip chip)
    {
        model.SetChip(chip);
    }

    public List<ChipMove> GetChipMoves()
    {
       return view.chipMoves;
    }

    #endregion
}

public interface IChipBank
{
    List<ChipMove> GetChipMoves();
}
