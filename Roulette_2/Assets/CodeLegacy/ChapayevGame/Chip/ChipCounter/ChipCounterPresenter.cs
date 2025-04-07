using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipCounterPresenter
{
    private readonly ChipCounterModel model;
    private readonly ChipCounterView view;

    public ChipCounterPresenter(ChipCounterModel model, ChipCounterView view)
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
        model.OnAddChip += view.AddChip;
        model.OnRemoveChip += view.RemoveChip;

        model.OnChangeCountChip += view.SetCount;
    }

    private void DeactivateEvents()
    {
        model.OnAddChip -= view.AddChip;
        model.OnRemoveChip -= view.RemoveChip;

        model.OnChangeCountChip -= view.SetCount;
    }

    #region Input

    public void AddChip(ChipMove chipMove)
    {
        model.AddChip(chipMove.Chip);
    }

    public void RemoveChip(ChipMove chipMove)
    {
        model.RemoveChip(chipMove.Chip);
    }

    #endregion
}
