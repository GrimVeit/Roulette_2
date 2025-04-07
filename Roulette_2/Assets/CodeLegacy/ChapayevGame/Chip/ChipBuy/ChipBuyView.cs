using System;
using UnityEngine;
using UnityEngine.UI;

public class ChipBuyView : View
{
    [SerializeField] private Button buttonBuyChip;
    [SerializeField] private Image imageChip;

    public void Initialize()
    {
        buttonBuyChip.onClick.AddListener(() => OnClickToBuy?.Invoke());
    }

    public void Dispose()
    {
        buttonBuyChip.onClick.RemoveListener(() => OnClickToBuy?.Invoke());
    }

    public void SetSelectChip(Chip chip)
    {
        imageChip.sprite = chip.Sprite;
    }

    #region Input

    public event Action OnClickToBuy;

    #endregion
}
