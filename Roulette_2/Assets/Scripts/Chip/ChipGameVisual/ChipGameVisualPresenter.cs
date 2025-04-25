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

    }

    private void DeactivasteEvents()
    {

    }

    #region Input

    public void SpawnChips(int id, List<int> positionsIndex)
    {

    }

    #endregion
}
