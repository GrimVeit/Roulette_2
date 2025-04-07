using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreStrategyPresenter : IStoreStrategyData
{
    private readonly StoreStrategyModel model;

    public StoreStrategyPresenter(StoreStrategyModel model)
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

    public event Action<Strategy> OnOpenNewStrategy
    {
        add => model.OnOpenNewStrategy += value;
        remove => model.OnOpenNewStrategy -= value;
    }

    public event Action<Strategy> OnOpenStrategy
    {
        add => model.OnOpenStrategy += value;
        remove => model.OnOpenStrategy -= value;
    }

    public event Action<Strategy> OnCloseStrategy
    {
        add => model.OnCloseStrategy += value;
        remove => model.OnCloseStrategy -= value;
    }

    public event Action<Strategy> OnDeselectStrategy
    {
        add => model.OnDeselectStrategy += value;
        remove => model.OnDeselectStrategy -= value;
    }

    public event Action<Strategy> OnSelectStrategy
    {
        add => model.OnSelectStrategy += value;
        remove => model.OnSelectStrategy -= value;
    }

    public bool IsAvailableStrategy()
    {
        return model.IsAvailableStrategy();
    }

    public Strategy GetRandomCloseStrategy()
    {
        return model.GetRandomCloseStrategy();
    }


    public void SelectStrategy(int id)
    {
        model.SelectStrategy(id);
    }

    public void UnselectAllStrategies()
    {
        model.UnselectAllStrategies();
    }

    public void OpenStrategy(int id)
    {
        model.OpenStrategy(id);
    }

    #endregion
}

public interface IStoreStrategyData
{
    public bool IsAvailableStrategy();

    public Strategy GetRandomCloseStrategy();
}
