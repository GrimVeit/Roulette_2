using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltitudePresenter
{
    private readonly AltitudeModel _model;
    private readonly AltitudeView _view;

    public AltitudePresenter(AltitudeModel model, AltitudeView view)
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
        _model.OnChangeAltitude += _view.SetAltitude;
    }

    private void DeactivateEvents()
    {
        _model.OnChangeAltitude -= _view.SetAltitude;
    }

    #region Input

    public void Clear()
    {
        _model.Clear();
    }

    public void ActivateAltitude()
    {
        _model.ActivateAltitude();
    }

    public void DeactivateAltitude()
    {
        _model.DeactivateAltitude();
    }

    #endregion
}
