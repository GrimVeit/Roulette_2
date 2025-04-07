using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class StoreStrategyModel
{
    public event Action<Strategy> OnOpenStrategy;
    public event Action<Strategy> OnOpenNewStrategy;
    public event Action<Strategy> OnCloseStrategy;

    public event Action<Strategy> OnDeselectStrategy;
    public event Action<Strategy> OnSelectStrategy;


    private StrategyGroup strategyGroup;
    private Strategy currentStrategy;

    private List<StrategyData> chipDatas = new List<StrategyData>();

    public readonly string FilePath = Path.Combine(Application.persistentDataPath, "Strategy.json");

    public StoreStrategyModel(StrategyGroup strategyGroup)
    {
        this.strategyGroup = strategyGroup;

        if (File.Exists(FilePath))
        {
            string loadedJson = File.ReadAllText(FilePath);
            StrategyDatas gameTypeDatas = JsonUtility.FromJson<StrategyDatas>(loadedJson);

            Debug.Log("Load data");

            this.chipDatas = gameTypeDatas.Datas.ToList();
        }
        else
        {
            Debug.Log("New Data");

            chipDatas = new List<StrategyData>();

            for (int i = 0; i < strategyGroup.Strategies.Count; i++)
            {
                if (i == 0)
                {
                    chipDatas.Add(new StrategyData(true, true));
                }
                else
                {
                    chipDatas.Add(new StrategyData(false, false));
                }
            }
        }

        for (int i = 0; i < strategyGroup.Strategies.Count; i++)
        {
            strategyGroup.Strategies[i].SetData(chipDatas[i]);
        }
    }

    public void Initialize()
    {
        for (int i = 0; i < strategyGroup.Strategies.Count; i++)
        {
            if (strategyGroup.Strategies[i].StrategyData.IsOpen)
                OnOpenStrategy?.Invoke(strategyGroup.Strategies[i]);
            else
                OnCloseStrategy?.Invoke(strategyGroup.Strategies[i]);

            if(strategyGroup.Strategies[i].StrategyData.IsSelect)
                OnSelectStrategy?.Invoke(strategyGroup.Strategies[i]);
            else
                OnDeselectStrategy?.Invoke(strategyGroup.Strategies[i]);
        }
    }

    public void Dispose()
    {
        string json = JsonUtility.ToJson(new StrategyDatas(chipDatas.ToArray()));
        File.WriteAllText(FilePath, json);
    }



    public void SelectStrategy(int id)
    {
        var strategy = strategyGroup.GetStrategyById(id);

        if (strategy == null)
        {
            Debug.LogError($"Not found strategy by id - {id}");
            return;
        }

        if(currentStrategy != null)
        {
            if(currentStrategy == strategy)
            {
                OnDeselectStrategy?.Invoke(currentStrategy);
                currentStrategy.StrategyData.IsSelect = false;
                currentStrategy = null;
                return;
            }
            else
            {
                OnDeselectStrategy?.Invoke(currentStrategy);
                currentStrategy.StrategyData.IsSelect = false;
                currentStrategy = strategy;
                OnSelectStrategy?.Invoke(currentStrategy);
                currentStrategy.StrategyData.IsSelect = true;
                return;
            }
        }
        else
        {
            currentStrategy = strategy;
            OnSelectStrategy?.Invoke(currentStrategy);
            currentStrategy.StrategyData.IsSelect = true;
        }
    }

    public void UnselectAllStrategies()
    {
        strategyGroup.Strategies.ForEach(data =>
        {
            if (data.StrategyData.IsSelect)
            {
                data.StrategyData.IsSelect = false;
                OnDeselectStrategy?.Invoke(data);
            }
        });
    }

    public void OpenStrategy(int number)
    {
        var chip = strategyGroup.GetStrategyById(number);

        if (chip == null)
        {
            Debug.LogError($"Not found strategy by id - {number}");
            return;
        }

        if (chip.StrategyData.IsOpen)
        {
            Debug.LogWarning($"Strategy by id - {number} is already open");
        }
        else
        {
            chip.StrategyData.IsOpen = true;
            OnOpenStrategy?.Invoke(chip);
            OnOpenNewStrategy?.Invoke(chip);
}
    }

    public bool IsAvailableStrategy()
    {
        return strategyGroup.IsAvailableStrategy();
    }

    public Strategy GetRandomCloseStrategy()
    {
        return strategyGroup.GetRandomCloseStrategy();
    }
}

[Serializable]
public class StrategyDatas
{
    public StrategyData[] Datas;

    public StrategyDatas(StrategyData[] datas)
    {
        Datas = datas;
    }
}

[Serializable]
public class StrategyData
{
    public bool IsOpen;
    public bool IsSelect;

    public StrategyData(bool isSelect, bool isOpen)
    {
        this.IsSelect = isSelect;
        this.IsOpen = isOpen;
    }
}
