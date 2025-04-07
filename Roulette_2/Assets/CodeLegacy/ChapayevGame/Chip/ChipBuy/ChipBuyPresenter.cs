using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipBuyPresenter
{
    private readonly ChipBuyModel model;
    private readonly ChipBuyView view;

    public ChipBuyPresenter(ChipBuyModel model, ChipBuyView view)
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

        model.OnSelectRandom_Value += view.SetSelectChip;
    }

    private void DeactivatEvents()
    {
        view.OnClickToBuy -= model.SelectRandomClose;

        model.OnSelectRandom_Value -= view.SetSelectChip;
    }

    #region Input

    public event Action OnSelectRandom
    {
        add => model.OnSelectRandom += value;
        remove => model.OnSelectRandom -= value;
    }

    public event Action<int> OnBuyChip
    {
        add => model.OnBuyChip += value;
        remove => model.OnBuyChip -= value;
    }

    public void Buy()
    {
        model.Buy();
    }

    public bool CanBuy()
    {
        return model.CanBuy();
    }

    #endregion
}
