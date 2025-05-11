using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationTaskModel
{
    public event Action<string> OnSendTask;

    private readonly ITaskCompletedProviderEvent _taskCompletedProvider;
    private readonly INotificationProvider _notificationProvider;
    private readonly ISoundProvider _soundProvider;

    public NotificationTaskModel(INotificationProvider notificationProvider, ITaskCompletedProviderEvent taskCompletedProviderEvent, ISoundProvider soundProvider)
    {
        _notificationProvider = notificationProvider;
        _taskCompletedProvider = taskCompletedProviderEvent;

        _taskCompletedProvider.OnComplete += SendTask;
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _taskCompletedProvider.OnComplete -= SendTask;
    }

    private void SendTask(Task task)
    {
        Debug.Log(task.GetID());

        OnSendTask?.Invoke(task.GetID());
    }

    public void SetTaskName(string taskName)
    {
        _notificationProvider.SendMessage($"<color=#ffd580>{taskName}</color>", "Daily Task Completed!", 1);
        _soundProvider.PlayOneShot("Success");
    }
}
