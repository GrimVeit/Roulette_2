using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class NicknameRandomModel
{
    public event Action OnSuccess;
    public event Action OnFailure;
    public event Action<string> OnCreateNickname;

    private readonly Regex mainRegex = new("^[a-zA-Z0-9._]*$");
    private readonly Regex invalidRegex = new(@"(\.{2,}|/{2,})");
    private const string URL = "https://usernameapiv1.vercel.app/api/random-usernames?count=1";

    private IEnumerator coroutineRequest;

    private List<string> nicknamesUsers = new();

    private int minLength;
    private int maxLength;

    public void CreateRandomNickname(int minLength, int maxLength)
    {
        this.minLength = minLength;
        this.maxLength = maxLength;

        if(coroutineRequest != null)
            Coroutines.Stop(coroutineRequest);

        coroutineRequest = WebRequest();
        Coroutines.Start(coroutineRequest);
    }

    private IEnumerator WebRequest()
    {
        UnityWebRequest request = UnityWebRequest.Get(URL);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("No randomizing nickname");
            OnFailure?.Invoke();
            yield break;
        }

        RandomNicknamesData data = JsonUtility.FromJson<RandomNicknamesData>(request.downloadHandler.text);

        nicknamesUsers = new List<string>(data.usernames);

        string nickname = nicknamesUsers[0];

        if (nickname.EndsWith("_"))
        {
            nickname += UnityEngine.Random.Range(0, 10);
        }

        Debug.Log(nickname);

        if (string.IsNullOrEmpty(nickname))
        {
            OnFailure?.Invoke();
            yield break;
        }

        if (nickname.Length < minLength)
        {
            Debug.LogError("Nickname must be at least 5 characters long");
            OnFailure?.Invoke();
            yield break;
        }

        if (nickname.Length > maxLength)
        {
            Debug.LogError("Nickname must not exceed 17 characters");
            OnFailure?.Invoke();
            yield break;
        }

        if (!mainRegex.IsMatch(nickname))
        {
            Debug.LogError("Nickname can only contain english letters, numbers, periods and slashes");
            OnFailure?.Invoke();
            yield break;
        }

        if (invalidRegex.IsMatch(nickname))
        {
            Debug.LogError("Nickname cannot contain consencutive periods and slashes");
            OnFailure?.Invoke();
            yield break;
        }

        if (nickname.EndsWith("."))
        {
            Debug.LogError("Nickname cannot end with a period");
            OnFailure?.Invoke();
            yield break;
        }

        OnCreateNickname?.Invoke(nickname);
        OnSuccess?.Invoke();
    }
}

public class RandomNicknamesData
{
    public string[] usernames;
}
