using System;
using UnityEngine;

public class BetPrepareModel
{
    public event Action OnPlay;

    public event Action OnActivate;
    public event Action OnDeactivate;

    private IMoneyProvider _moneyProvider;

    private float _currentBet = 0;

    private bool isActive = true;

    public BetPrepareModel(IMoneyProvider moneyProvider)
    {
        _moneyProvider = moneyProvider;
    }

    public void SetBet(float bet)
    {
        _currentBet = MathF.Round(bet, 1);

        if(!isActive) return;

        if (_moneyProvider.CanAfford(_currentBet))
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
        isActive = true;

        if (_moneyProvider.CanAfford(_currentBet))
        {
            OnActivate?.Invoke();
        }
    }

    public void Deactivate()
    {
        isActive = false;

        OnDeactivate?.Invoke();
    }

    public void Play()
    {
        Debug.Log(-_currentBet);

        _moneyProvider.SendMoney( - _currentBet);

        OnPlay?.Invoke();
    }
}
