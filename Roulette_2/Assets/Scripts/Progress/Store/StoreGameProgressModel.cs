using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class StoreGameProgressModel
{
    public event Action<int> OnOpenGame;
    public event Action<int, bool> OnChangeStatusGame;

    private List<GameProgressData> gameProgressDatas = new();

    public readonly string FilePath = Path.Combine(Application.persistentDataPath, "GameProgress.json");

    public void Initialize()
    {
        if (File.Exists(FilePath))
        {
            string loadedJson = File.ReadAllText(FilePath);
            GameProgressDatas gameProgressDatas = JsonUtility.FromJson<GameProgressDatas>(loadedJson);

            this.gameProgressDatas = gameProgressDatas.Datas.ToList();
        }
        else
        {
            gameProgressDatas = new List<GameProgressData>();

            for (int i = 0; i < 7; i++)
            {
                if (i == 0)
                {
                    gameProgressDatas.Add(new GameProgressData(true, false));
                }
                else
                {
                    gameProgressDatas.Add(new GameProgressData(false, false));
                }
            }
        }

        for (int i = 0; i < gameProgressDatas.Count; i++)
        {
            OnChangeStatusGame?.Invoke(i, gameProgressDatas[i].IsOpen);
        }
    }

    public void Dispose()
    {
        string json = JsonUtility.ToJson(new GameProgressDatas(gameProgressDatas.ToArray()));
        File.WriteAllText(FilePath, json);
    }

    public void OpenGame(int id)
    {
        var gameProgress = gameProgressDatas[id];

        if(gameProgress == null)
        {
            Debug.LogError("Not found game progress with id - " + id);
            return;
        }

        if(gameProgress.IsOpen) return;

        gameProgress.IsOpen = true;
        Debug.Log(id);
        OnOpenGame?.Invoke(id);
        OnChangeStatusGame?.Invoke(id, gameProgress.IsOpen);
    }

    public void CompleteTutuorial(int id)
    {
        var gameProgress = gameProgressDatas[id];

        if (gameProgress == null)
        {
            Debug.LogError("Not found game progress with id - " + id);
            return;
        }

        gameProgressDatas[id].HasPlayedTutorial = true;
    }

    public bool HasPlayedTutorialById(int id)
    {
        var gameProgress = gameProgressDatas[id];

        if (gameProgress == null)
        {
            Debug.LogError("Not found game progress with id - " + id);
            return false;
        }

        return gameProgress.HasPlayedTutorial;
    }
}

[Serializable]
public class GameProgressDatas
{
    public GameProgressData[] Datas;

    public GameProgressDatas(GameProgressData[] datas)
    {
        Datas = datas;
    }
}

[Serializable]
public class GameProgressData
{
    public bool IsOpen;
    public bool HasPlayedTutorial;

    public GameProgressData(bool isOpen, bool hasPlayedTutorial)
    {
        IsOpen = isOpen;
        HasPlayedTutorial = hasPlayedTutorial;
    }
}
