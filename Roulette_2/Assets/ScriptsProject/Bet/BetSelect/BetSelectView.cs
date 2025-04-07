using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetSelectView : View
{
    [SerializeField] private Button buttonIncrease;
    [SerializeField] private Button buttonDecrease;
    [SerializeField] private TextMeshProUGUI textBet;

    public void Initialize()
    {
        buttonIncrease.onClick.AddListener(()=> OnIncrease?.Invoke());
        buttonDecrease.onClick.AddListener(()=> OnDecrease?.Invoke());
    }

    public void Dispose()
    {
        buttonIncrease.onClick.RemoveListener(()=> OnIncrease?.Invoke());
        buttonDecrease.onClick.RemoveListener(()=> OnDecrease?.Invoke());
    }

    #region Input

    public event Action OnIncrease;
    public event Action OnDecrease;

    #endregion

    #region Output

    public void SetBet(float bet)
    {
        textBet.text = bet.ToString();
    }

    #endregion
}
