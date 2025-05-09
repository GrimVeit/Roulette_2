using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationPresenter : INotificationProvider
{
    private readonly NotificationModel _model;
    private readonly NotificationView _view;

    public NotificationPresenter(NotificationModel model, NotificationView view)
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
        _model.OnSendMessage += _view.SendNotification;
    }

    private void DeactivateEvents()
    {
        _model.OnSendMessage -= _view.SendNotification;
    }

    #region Input

    public void SendMessage(string description, string header, int type)
    {
        _model.SendMessage(description, header, type);
    }

    #endregion
}

public interface INotificationProvider
{
    void SendMessage(string description, string header, int type);
}
