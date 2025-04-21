using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteValueHistoryPresenter
{
    private readonly RouletteValueHistoryModel _model;
    private readonly RouletteValueHistoryView _view;

    public RouletteValueHistoryPresenter(RouletteValueHistoryModel model, RouletteValueHistoryView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _model.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
    }

    private void ActivateEvents()
    {
        _model.OnSlotValueChanged += _view.SetRouletteNumber;
    }

    private void DeactivateEvents()
    {
        _model.OnSlotValueChanged -= _view.SetRouletteNumber;
    }

    #region Input

    public void ClearAll()
    {
        _view.ClearValues();
    }

    #endregion
}
