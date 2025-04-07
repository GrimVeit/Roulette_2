using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotStoreStrategyPresenter
{
    private readonly BotStoreStrategyModel model;

    public BotStoreStrategyPresenter(BotStoreStrategyModel model)
    {
        this.model = model;
    }

    public void Initialize()
    {
        model.Initialize();
    }

    public void Dispose()
    {
        model.Dispose();
    }


    #region Input

    public event Action<Strategy> OnSelectRandomStrategy
    {
        add => model.OnSelectRandomStrategy += value;
        remove => model.OnSelectRandomStrategy -= value;
    }

    public void SelectRandomStrategy(Strategy strategy)
    {
        model.SelectRandomStrategyByChipCount(strategy.ChipCount);
    }

    #endregion
}
