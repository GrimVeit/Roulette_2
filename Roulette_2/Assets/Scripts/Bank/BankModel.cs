using System;
using UnityEngine;

public class BankModel
{
    public float Money => money;

    private float money;
    public event Action OnAddMoney;
    public event Action OnRemoveMoney;
    public event Action<float> OnChangeMoney;

    private const string BANK_MONEY = "BANK_MONEY";

    public void Initialize()
    {
        money = PlayerPrefs.GetFloat(BANK_MONEY, 100);
    }

    public void Destroy()
    {
        PlayerPrefs.SetFloat(BANK_MONEY, money);
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
        this.money += money;
        this.money = Mathf.Round(this.money * 10f) / 10f;
        MathF.Round(this.money, 1);
        OnChangeMoney?.Invoke(this.money);

        Debug.Log(this.money);
    }

    public bool CanAfford(float bet)
    {
        return money >= bet;
    }
}
