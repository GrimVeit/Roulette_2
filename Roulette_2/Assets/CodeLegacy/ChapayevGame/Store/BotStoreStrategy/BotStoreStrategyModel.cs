using System;
using UnityEngine;

public class BotStoreStrategyModel
{
    public event Action<Strategy> OnSelectRandomStrategy;

    private StrategyGroup strategyGroup;

    public BotStoreStrategyModel(StrategyGroup strategyGroup)
    {
        this.strategyGroup = strategyGroup;
    }

    public void Initialize()
    {
        
    }

    public void Dispose()
    {

    }

    public void SelectRandomStrategyByChipCount(int count)
    {
        var strategy = strategyGroup.GetStrategyByChipCount(count);

        if (strategy == null)
        {
            Debug.LogError($"Not found strategy by chip count - {count}");
            return;
        }

        OnSelectRandomStrategy?.Invoke(strategy);
    }
}
