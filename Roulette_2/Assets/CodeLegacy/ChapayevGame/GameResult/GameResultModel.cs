using System;
using UnityEngine;

public class GameResultModel
{
    public event Action<int> OnWin_Value;
    public event Action OnWin;
    public event Action OnLose;

    private int countChipsPlayer = 0;
    private int countChipsBot = 0;

    private WinPrices winPrices;

    private bool isEndGame;

    public IMoneyProvider moneyProvider;

    public GameResultModel(WinPrices winPrices, IMoneyProvider moneyProvider)
    {
        this.winPrices = winPrices;
        this.moneyProvider = moneyProvider;
    }

    public void AddPlayerChip()
    {
        if (isEndGame) return;

        countChipsPlayer += 1;
    }

    public void RemovePlayerChip()
    {
        if (isEndGame) return;

        countChipsPlayer -= 1;

        CheckGameResult();
    }

    public void AddBotChip()
    {
        if (isEndGame) return;

        countChipsBot += 1;
    }

    public void RemoveBotChip()
    {
        if (isEndGame) return;

        countChipsBot -= 1;

        CheckGameResult();
    }

    private void CheckGameResult()
    {
        if(countChipsPlayer == 0)
        {
            OnLose?.Invoke();
            isEndGame = true;
            Debug.Log("LOSE GAME");
        }

        if(countChipsBot == 0)
        {
            if(countChipsPlayer >= 1)
            {
                int win = winPrices.GetWinPriceByChipCount(countChipsPlayer).Win;
                moneyProvider.SendMoney(win);
                OnWin_Value?.Invoke(win);
                OnWin?.Invoke();
                isEndGame = true;
                Debug.Log("WIN GAME - " + countChipsPlayer);
            }
        }
    }

    public bool IsPlayerWin()
    {
        if(countChipsPlayer == 0)
        {
            return false;
        }

        if(countChipsBot == 0)
        {
            return true;
        }

        return false;
    }
}
