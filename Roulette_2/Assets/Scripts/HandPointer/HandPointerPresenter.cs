using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPointerPresenter : IHandPointerProvider
{
    private readonly HandPointerModel _model;
    private readonly HandPointerView _view;

    public HandPointerPresenter(HandPointerModel model, HandPointerView view)
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
        _model.OnActivate += _view.Activate;
        _model.OnDeactivate += _view.Deactivate;
        _model.OnMove += _view.SetData;
    }

    private void DeactivateEvents()
    {
        _model.OnActivate -= _view.Activate;
        _model.OnDeactivate -= _view.Deactivate;
        _model.OnMove -= _view.SetData;
    }

    #region Input

    public void Move(int id)
    {
        _model.Move(id);
    }

    public void Activate()
    {
        _model.Activate();
    }

    public void Deactivate()
    {
        _model.Deactivate();
    }

    #endregion
}

public interface IHandPointerProvider
{
    void Activate();
    void Deactivate();
    void Move(int id);
}
