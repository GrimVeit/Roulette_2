using System;
using UnityEngine;
using UnityEngine.UI;

public class StrategyBuyView : View
{
    [SerializeField] private Button buttonBuyStrategy;
    [SerializeField] private Image imageStrategy;

    public void Initialize()
    {
        buttonBuyStrategy.onClick.AddListener(()=> OnClickToBuy?.Invoke());
    }

    public void Dispose()
    {
        buttonBuyStrategy.onClick.RemoveListener(() => OnClickToBuy?.Invoke());
    }

    public void SetSelectStrategy(Strategy strategy)
    {
        imageStrategy.sprite = strategy.Sprite;
    }

    #region Input

    public event Action OnClickToBuy;

    #endregion
}
