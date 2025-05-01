using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipGameCountVisualPresenter
{
    private readonly ChipGameCountVisualModel _model;
    private readonly ChipGameCountVisualView _view;

    public ChipGameCountVisualPresenter(ChipGameCountVisualModel model, ChipGameCountVisualView view)
    {
        _model = model;
        _view = view;

        ActivateEvents();
    }

    public void Initialize()
    {
        _model.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
    }

    private void ActivateEvents()
    {
        _model.OnChangeChipsCount += _view.SetData;
        _model.OnHideChips += _view.Hide;
        _model.OnShowChips += _view.Show;
    }

    private void DeactivateEvents()
    {
        _model.OnChangeChipsCount -= _view.SetData;
        _model.OnHideChips -= _view.Hide;
        _model.OnShowChips -= _view.Show;
    }
}
