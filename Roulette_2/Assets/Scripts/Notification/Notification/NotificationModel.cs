using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationModel
{
    public event Action<string, string, int> OnSendMessage;

    public  void SendMessage(string description, string title, int type)
    {
        OnSendMessage?.Invoke(description, title, type);
    }
}
