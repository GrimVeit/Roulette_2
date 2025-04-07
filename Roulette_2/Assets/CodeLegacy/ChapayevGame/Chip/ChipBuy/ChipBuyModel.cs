using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipBuyModel
{
    public event Action OnSelectRandom;
    public event Action<Chip> OnSelectRandom_Value;

    public event Action<int> OnBuyChip;
    public event Action OnClickToBuy;

    private readonly IMoneyProvider moneyProvider;
    private readonly IStoreChipData storeChipData;

    private Chip currentChooseChip;

    public ChipBuyModel(IMoneyProvider moneyProvider, IStoreChipData storeStrategyData)
    {
        this.moneyProvider = moneyProvider;
        this.storeChipData = storeStrategyData;
    }

    public void SelectRandomClose()
    {
        if (!storeChipData.IsAvailableChip()) return;

        if (CanBuy() == false) return;

        currentChooseChip = storeChipData.GetRandomCloseChip();

        if (currentChooseChip == null) return;

        OnSelectRandom_Value?.Invoke(currentChooseChip);
        OnSelectRandom?.Invoke();
    }

    public void Buy()
    {
        if (currentChooseChip == null) return;

        moneyProvider.SendMoney(-500);
        OnBuyChip?.Invoke(currentChooseChip.ID);
    }

    public bool CanBuy()
    {
        return moneyProvider.CanAfford(500);
    }
}
