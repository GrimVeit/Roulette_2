using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyBuyVisualizePresenter
{
    private readonly StrategyBuyVisualizeModel model;
    private readonly StrategyBuyVisualizeView view;

    public StrategyBuyVisualizePresenter(StrategyBuyVisualizeModel model, StrategyBuyVisualizeView view)
    {
        this.model = model;
        this.view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        view.Dispose();
    }

    private void ActivateEvents()
    {
        model.OnSetOpenStrategy += view.SetOpenStrategyBuyVisualize;
        model.OnSetOpenNewStrategy += view.SetOpenNewStrategyBuyVisualize;

        model.OnSetCloseStrategy += view.SetCloseStrategyBuyVisualize;
    }

    private void DeactivateEvents()
    {
        model.OnSetOpenStrategy -= view.SetOpenStrategyBuyVisualize;
        model.OnSetOpenNewStrategy -= view.SetOpenNewStrategyBuyVisualize;

        model.OnSetCloseStrategy -= view.SetCloseStrategyBuyVisualize;
    }

    #region Input

    public void SetOpenStrategy(Strategy strategy)
    {
        model.SetOpenStrategy(strategy);
    }

    public void SetOpenNewStrategy(Strategy strategy)
    {
        model.SetOpenNewStrategy(strategy);
    }

    public void SetCloseStrategy(Strategy strategy)
    {
        model.SetCloseStrategy(strategy);
    }

    #endregion
}
