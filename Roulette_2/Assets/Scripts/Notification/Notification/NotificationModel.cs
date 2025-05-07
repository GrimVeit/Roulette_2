using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationModel
{
    public event Action<string, string> OnSendMessage;

    public  void SendMessage(string description, string title)
    {
        OnSendMessage?.Invoke(description, title);
    }
}
