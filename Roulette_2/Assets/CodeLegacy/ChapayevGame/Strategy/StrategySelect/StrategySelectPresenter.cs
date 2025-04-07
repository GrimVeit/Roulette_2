using System;

public class StrategySelectPresenter
{
    private readonly StrategySelectModel model;
    private readonly StrategySelectView view;

    public StrategySelectPresenter(StrategySelectModel model, StrategySelectView view)
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
        view.OnChooseStrategy += model.ChooseStrategy;

        model.OnSetOpenStrategy += view.SetOpenStrategy;
        model.OnSetOpenNewStrategy += view.SetOpenNewStrategy;
        model.OnSelectStrategy += view.SelectStrategy;
        model.OnDeselectStrategy += view.DeselectStrategy;
    }

    private void DeactivateEvents()
    {
        view.OnChooseStrategy -= model.ChooseStrategy;

        model.OnSetOpenStrategy -= view.SetOpenStrategy;
        model.OnSetOpenNewStrategy -= view.SetOpenNewStrategy;
        model.OnSelectStrategy -= view.SelectStrategy;
        model.OnDeselectStrategy -= view.DeselectStrategy;
    }

    #region Input

    public event Action<int> OnChooseStrategy
    {
        add => model.OnChooseStrategy += value;
        remove => model.OnChooseStrategy -= value;
    }

    public void SelectStrategy(Strategy strategy)
    {
        model.SelectStrategy(strategy.ID);
    }

    public void DeselectStrategy(Strategy strategy)
    {
        model.DeselectStrategy(strategy.ID);
    }

    public void SetOpenStrategy(Strategy strategy)
    {
        model.SetOpenStrategy(strategy);
    }

    public void SetOpenNewStrategy(Strategy strategy)
    {
        model.SetOpenNewStrategy(strategy);
    }

    #endregion
}
