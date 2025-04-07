using System;
public class StrategyBuyModel
{
    public event Action OnSelectRandom;
    public event Action<Strategy> OnSelectRandom_Value;

    public event Action<int> OnBuyStrategy;
    public event Action OnClickToBuy;

    private readonly IMoneyProvider moneyProvider;
    private readonly IStoreStrategyData storeStrategyData;

    private Strategy currentChooseStrategy;

    public StrategyBuyModel(IMoneyProvider moneyProvider, IStoreStrategyData storeStrategyData)
    {
        this.moneyProvider = moneyProvider;
        this.storeStrategyData = storeStrategyData;
    }

    public void SelectRandomClose()
    {
        if (!storeStrategyData.IsAvailableStrategy()) return;

        if(CanBuy() == false) return;

        currentChooseStrategy = storeStrategyData.GetRandomCloseStrategy();

        if(currentChooseStrategy == null) return;

        OnSelectRandom_Value?.Invoke(currentChooseStrategy);
        OnSelectRandom?.Invoke();
    }

    public void Buy()
    {
        if(currentChooseStrategy == null) return;

        moneyProvider.SendMoney(-500);
        OnBuyStrategy?.Invoke(currentChooseStrategy.ID);
    }



    public bool CanBuy()
    {
        return moneyProvider.CanAfford(500);
    }
}
