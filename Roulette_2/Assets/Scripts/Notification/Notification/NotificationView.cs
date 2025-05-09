using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationView : View
{
    [SerializeField] private List<Notification> notificationPrefabs = new List<Notification>();
    [SerializeField] private Transform transformSpawnNotification;

    private Notification _currentNotification;

    private IEnumerator coroutineTimer;

    public void SendNotification(string description, string title, int type)
    {
        if (coroutineTimer != null) Coroutines.Stop(coroutineTimer);

        coroutineTimer = Timer(3);
        Coroutines.Start(coroutineTimer);

        Deactivate(_currentNotification);

        var prefab = notificationPrefabs[type];

        _currentNotification = Instantiate(prefab, transformSpawnNotification);
        _currentNotification.OnDeactivate += Deactivate;
        _currentNotification.SetData(description, title);
        _currentNotification.Initialize();
        _currentNotification.Activate();
    }

    private void Deactivate(Notification notification)
    {
        if(_currentNotification == null) return;

        _currentNotification.OnDeactivate -= Deactivate;
        _currentNotification.Deactivate();
    }

    private IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Deactivate(_currentNotification);
    }
}