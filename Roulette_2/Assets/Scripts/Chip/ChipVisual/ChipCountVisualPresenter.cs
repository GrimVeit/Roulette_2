using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipCountVisualPresenter
{
    private readonly ChipCountVisualModel _model;
    private readonly ChipCountVisualView _view;

    public ChipCountVisualPresenter(ChipCountVisualModel model, ChipCountVisualView view)
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
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        _model.OnChangeChipsCount += _view.SetData;
    }

    private void DeactivateEvents()
    {
        _model.OnChangeChipsCount -= _view.SetData;
    }

    #region Input

    public void ChangeChipsCount(int idChips, int count)
    {
        _model.ChangeChipsCount(idChips, count);
    }

    #endregion
}
