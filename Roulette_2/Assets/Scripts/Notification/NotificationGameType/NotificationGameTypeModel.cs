using System;
using System.Collections.Generic;
using UnityEngine;

public class NotificationGameTypeModel
{
    public event Action<int> OnSendGameType;

    private readonly IGameProgressProvider_Read _gameProgressProvider;
    private readonly INotificationProvider _notificationProvider;
    private readonly ISoundProvider _soundProvider;
    private readonly List<string> strings = new()
    {
        "Ready to try your luck?",
        "Explore the excitement!",
        "Ready to dive in?"

    };

    public NotificationGameTypeModel(INotificationProvider notificationProvider, IGameProgressProvider_Read gameProgressProvider, ISoundProvider soundProvider)
    {
        _notificationProvider = notificationProvider;
        _gameProgressProvider = gameProgressProvider;

        _gameProgressProvider.OnOpenGame += SendGameType;
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {
        
    }

    public void Dispose()
    {
        _gameProgressProvider.OnOpenGame -= SendGameType;
    }

    private void SendGameType(int id)
    {
        Debug.Log(id);

        OnSendGameType?.Invoke(id);
    }

    public void SetGameTypeName(string gameTypeName)
    {
        _notificationProvider.SendMessage($"You've unlocked the <color=#ffd580>{gameTypeName}</color> mode. {strings[UnityEngine.Random.Range(0, strings.Count)]}", "New Game Mode!", 0);
        _soundProvider.PlayOneShot("Success");
    }
}
