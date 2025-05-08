using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NotificationTaskView : View
{
    [SerializeField] private List<NotificationTask> notificationGameTypes = new List<NotificationTask>();

    public void ChooseGameType(string id)
    {
        var gameType = GetNotificationTaskById(id);

        if (gameType == null)
        {
            Debug.LogWarning("Not found notification task with id - " + id);
            return;
        }

        OnChooseTask?.Invoke(gameType.NameTask);
    }

    private NotificationTask GetNotificationTaskById(string id)
    {
        return notificationGameTypes.FirstOrDefault(data => data.Id == id);
    }

    #region Output

    public event Action<string> OnChooseTask;

    #endregion
}

[Serializable]
public class NotificationTask
{
    [SerializeField] private string id;
    [SerializeField] private string nameTask;

    public string Id => id;
    public string NameTask => nameTask;
}
