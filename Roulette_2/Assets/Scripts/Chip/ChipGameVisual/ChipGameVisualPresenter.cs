using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipGameVisualPresenter
{
    private readonly ChipGameVisualModel _model;
    private readonly ChipGameVisualView _view;

    public ChipGameVisualPresenter(ChipGameVisualModel model, ChipGameVisualView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivasteEvents();
    }

    private void ActivateEvents()
    {
        _model.OnAddChip += _view.AddChip;
        _model.OnReturnChip += _view.ReturnChip;
    }

    private void DeactivasteEvents()
    {
        _model.OnAddChip -= _view.AddChip;
        _model.OnReturnChip -= _view.ReturnChip;
    }

    #region Input

    public void SpawnChips(int id, List<int> positionsIndex)
    {

    }

    #endregion
}
