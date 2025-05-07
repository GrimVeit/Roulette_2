using System.Collections;
using UnityEngine;

public class NotificationView : View
{
    [SerializeField] private Notification notificationPrefab;
    [SerializeField] private Transform transformSpawnNotification;

    private Notification _currentNotification;

    private IEnumerator coroutineTimer;

    public void SendNotification(string description, string title)
    {
        if (coroutineTimer != null) Coroutines.Stop(coroutineTimer);

        coroutineTimer = Timer(3);
        Coroutines.Start(coroutineTimer);

        Deactivate(_currentNotification);

        _currentNotification = Instantiate(notificationPrefab, transformSpawnNotification);
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