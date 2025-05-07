using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NotificationGameTypeView : View
{
    [SerializeField] private List<NotificationGameType> notificationGameTypes = new List<NotificationGameType>();

    public void ChooseGameType(int id)
    {
        var gameType = GetNotificationGameTypeById(id);

        if(gameType == null)
        {
            Debug.LogWarning("Not found notification game type with id - " + id);
            return;
        }

        OnChooseGameType?.Invoke(gameType.NameGameType);
    }

    private NotificationGameType GetNotificationGameTypeById(int id)
    {
        return notificationGameTypes.FirstOrDefault(data => data.Id == id);
    }

    #region Output

    public event Action<string> OnChooseGameType;

    #endregion
}

[Serializable]
public class NotificationGameType
{
    [SerializeField] private int id;
    [SerializeField] private string nameGameType;

    public int Id => id;
    public string NameGameType => nameGameType;
}
