using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationTaskPresenter
{
    private readonly NotificationTaskModel _model;
    private readonly NotificationTaskView _view;

    public NotificationTaskPresenter(NotificationTaskModel model, NotificationTaskView view)
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
        _view.OnChooseTask += _model.SetTaskName;

        _model.OnSendTask += _view.ChooseGameType;
    }

    private void DeactivateEvents()
    {
        _view.OnChooseTask -= _model.SetTaskName;

        _model.OnSendTask -= _view.ChooseGameType;
    }
}
