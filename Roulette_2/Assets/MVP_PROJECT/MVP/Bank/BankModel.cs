using System;
using UnityEngine;

public class BankModel
{
    public float Money { get; private set; }
    public event Action OnAddMoney;
    public event Action OnRemoveMoney;
    public event Action<float> OnChangeMoney;

    private const string BANK_MONEY = "BANK_MONEY";

    public void Initialize()
    {
        Money = PlayerPrefs.GetFloat(BANK_MONEY, 20);
    }

    public void Destroy()
    {
        PlayerPrefs.SetFloat(BANK_MONEY, Money);
    }

    public void SendMoney(float money)
    {
        Debug.Log(money);

        if(money >= 0)
        {
            OnAddMoney?.Invoke();
        }
        else
        {
            OnRemoveMoney?.Invoke();
        }
        Money += money;
        Money = Mathf.Round(Money * 10f) / 10f;
        MathF.Round(Money, 1);
        OnChangeMoney?.Invoke(Money);

        Debug.Log(Money);
    }

    public bool CanAfford(float bet)
    {
        return Money >= bet;
    }
}
