using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActivateModel
{
    private IMoneyProvider _moneyProvider;
    private float currentBet;

    private bool isActivate;

    public GameActivateModel(IMoneyProvider moneyProvider)
    {
        _moneyProvider = moneyProvider;
    }

    public void SetBet(float bet)
    {
        currentBet = bet;
        if (_moneyProvider.CanAfford(currentBet))
        {
            OnActivate?.Invoke();
        }
        else
        {
            OnDeactivate?.Invoke();
        }
    }

    public void Activate()
    {
        if (_moneyProvider.CanAfford(currentBet))
        {
            OnActivate?.Invoke();
            return;
        }

        OnDeactivate?.Invoke();
    }

    public void Deactivate()
    {
        OnDeactivate?.Invoke();
    }

    public void PlayGame()
    {
        OnPlayGame?.Invoke();
    }

    #region Output

    public event Action OnPlayGame;

    public event Action OnActivate;
    public event Action OnDeactivate;

    #endregion
}
