using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPresenter
{
    private readonly PlatformModel _model;
    private readonly PlatformView _view;

    public PlatformPresenter(PlatformModel model, PlatformView view)
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
        _model.OnActivatePlatform += _view.ActivatePlatform;
        _model.OnDeactivatePlatform += _view.DeactivatePlatform;
    }

    private void DeactivateEvents()
    {
        _model.OnActivatePlatform -= _view.ActivatePlatform;
        _model.OnDeactivatePlatform -= _view.DeactivatePlatform;
    }

    #region Input

    public void ActivatePlatform()
    {
        _model.ActivatePlatform();
    }

    public void DeactivatePlatform()
    {
        _model.DeactivatePlatform();
    }

    #endregion
}
