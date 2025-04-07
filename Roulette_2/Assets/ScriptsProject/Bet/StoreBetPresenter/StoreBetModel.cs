using System;
using UnityEngine;

public class StoreBetModel
{
    private float currentBet;

    private readonly float minBet = 2.4f;
    private readonly string KEY;

    private bool isActive = false;

    public StoreBetModel(string key, float minBet)
    {
        this.KEY = key;
        this.minBet = minBet;
    }

    public void Initialize()
    {
        currentBet = PlayerPrefs.GetFloat(KEY, minBet);
        OnChooseBet?.Invoke(currentBet);
    }

    public void Dispose()
    {
        PlayerPrefs.SetFloat(KEY, currentBet);
    }

    #region Input

    public void IncreaseBet()
    {
        if(!isActive) return;

        currentBet = MathF.Round(currentBet += 0.2f, 1);

        OnChooseBet?.Invoke(currentBet);
    }

    public void DecreaseBet()
    {
        if(!isActive) return;

        if(currentBet == minBet) return;

        currentBet = MathF.Round(currentBet -= 0.2f, 1);

        if(currentBet < minBet)
        {
            currentBet = minBet;
        }

        OnChooseBet?.Invoke(currentBet);
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }

    #endregion

    #region Output

    public event Action<float> OnChooseBet;

    #endregion
}
