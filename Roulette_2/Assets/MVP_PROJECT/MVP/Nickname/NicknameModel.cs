using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class NicknameModel
{
    public event Action<string> OnGetNickname;

    public event Action OnCorrectNickname;
    public event Action OnIncorrectNickname;
    public event Action<string> OnEnterRegisterLoginError;

    public event Action<string> OnGetRandomNickname;

    private readonly Regex mainRegex = new("^[a-zA-Z0-9._]*$");
    private readonly Regex invalidRegex = new(@"(\.{2,}|/{2,})");
    private const string URL = "https://dinoipsum.com/api/?format=text&paragraphs=1&words=1";

    public string Nickname { get; private set; }

    private ISoundProvider soundProvider;

    public NicknameModel(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void ChangeNickname(string value)
    {
        Nickname = value;
        OnGetNickname?.Invoke(Nickname);

        //soundProvider.PlayOneShot("TextEnter");

        if (value.Length < 5)
        {
            OnEnterRegisterLoginError?.Invoke("Nickname must be at least 5 characters long");
            OnIncorrectNickname?.Invoke();
            return;
        }

        if (value.Length > 17)
        {
            OnEnterRegisterLoginError?.Invoke("Nickname must not exceed 17 characters");
            OnIncorrectNickname?.Invoke();
            return;
        }

        if (!mainRegex.IsMatch(value))
        {
            OnEnterRegisterLoginError?.Invoke("Nickname can only contain english letters, numbers, periods and slashes");
            OnIncorrectNickname?.Invoke();
            return;
        }

        if (invalidRegex.IsMatch(value))
        {
            OnEnterRegisterLoginError?.Invoke("Nickname cannot contain consencutive periods and slashes");
            OnIncorrectNickname?.Invoke();
            return;
        }

        if (value.EndsWith("."))
        {
            OnEnterRegisterLoginError?.Invoke("Nickname cannot end with a period");
            return;
        }

        OnEnterRegisterLoginError?.Invoke("");
        OnCorrectNickname?.Invoke();
    }
}
