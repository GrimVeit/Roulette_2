using System;

public class StrategyBuyVisualizeModel
{
    public event Action<Strategy> OnSetOpenStrategy;
    public event Action<Strategy> OnSetOpenNewStrategy;

    public event Action<Strategy> OnSetCloseStrategy;

    public void SetOpenStrategy(Strategy strategy)
    {
        OnSetOpenStrategy?.Invoke(strategy);
    }

    public void SetOpenNewStrategy(Strategy strategy)
    {
        OnSetOpenNewStrategy?.Invoke(strategy);
    }

    public void SetCloseStrategy(Strategy strategy)
    {
        OnSetCloseStrategy?.Invoke(strategy);
    }
}
