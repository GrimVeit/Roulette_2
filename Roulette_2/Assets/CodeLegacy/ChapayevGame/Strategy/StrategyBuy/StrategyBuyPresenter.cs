using System;

public class StrategyBuyPresenter
{
    private StrategyBuyModel model;
    private StrategyBuyView view;

    public StrategyBuyPresenter(StrategyBuyModel model, StrategyBuyView view)
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
        DeactivatEvents();

        view.Dispose();
    }

    private void ActivateEvents()
    {
        view.OnClickToBuy += model.SelectRandomClose;

        model.OnSelectRandom_Value += view.SetSelectStrategy;
    }

    private void DeactivatEvents()
    {
        view.OnClickToBuy -= model.SelectRandomClose;

        model.OnSelectRandom_Value -= view.SetSelectStrategy;
    }

    #region Input

    public event Action OnSelectRandom
    {
        add => model.OnSelectRandom += value;
        remove => model.OnSelectRandom -= value;
    }

    public event Action<int> OnBuyStrategy
    {
        add => model.OnBuyStrategy += value;
        remove => model.OnBuyStrategy -= value;
    }

    public void Buy()
    {
        model.Buy();
    }

    #endregion
}
