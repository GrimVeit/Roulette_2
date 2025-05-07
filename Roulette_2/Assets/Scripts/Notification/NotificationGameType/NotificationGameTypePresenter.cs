using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationGameTypePresenter
{
    private readonly NotificationGameTypeModel _model;
    private readonly NotificationGameTypeView _view;

    public NotificationGameTypePresenter(NotificationGameTypeModel model, NotificationGameTypeView view)
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
        _view.OnChooseGameType += _model.SetGameTypeName;

        _model.OnSendGameType += _view.ChooseGameType;
    }

    private void DeactivateEvents()
    {
        _view.OnChooseGameType -= _model.SetGameTypeName;

        _model.OnSendGameType -= _view.ChooseGameType;
    }
}
