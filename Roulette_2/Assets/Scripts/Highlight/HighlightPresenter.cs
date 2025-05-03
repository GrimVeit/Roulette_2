using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightPresenter : IHighlightProvider
{
    private readonly HighlightModel _model;
    private readonly HighlightView _view;

    public HighlightPresenter(HighlightModel model, HighlightView view)
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
        _model.OnSelect += _view.Select;
        _model.OnDeselect += _view.Deselect;
        _model.OnDeselectAll += _view.DeselectAll;
    }

    private void DeactivateEvents()
    {
        _model.OnSelect -= _view.Select;
        _model.OnDeselect -= _view.Deselect;
        _model.OnDeselectAll -= _view.DeselectAll;
    }

    #region Input

    public void Select(int id)
    {
        _model.Select(id);
    }

    public void Deselect(int id)
    {
        _model.Deselect(id);
    }

    public void DeselectAll()
    {
        _model.DeselectAll();
    }

    #endregion
}

public interface IHighlightProvider
{
    void Select(int id);
    void Deselect(int id);
    void DeselectAll();
}