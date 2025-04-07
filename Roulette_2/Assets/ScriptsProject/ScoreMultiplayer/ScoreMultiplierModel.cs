using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMultiplierModel
{
    public event Action<float> OnChangeScoreMultipliyer;

    private List<IScoreMultiplyProvider> scoreMultiplies = new List<IScoreMultiplyProvider>();

    private float multipliyerValue = 1;

    public void Initialize()
    {
        Clear();
    }

    public void Dispose()
    {

    }

    public void Clear()
    {
        if(scoreMultiplies.Count > 0)
        {
            for(int i = 0; i < scoreMultiplies.Count; i++)
            {
                scoreMultiplies[i].OnApplyScoreMultiply -= ApplyScore;
            }

            scoreMultiplies.Clear();
        }

        multipliyerValue = 1f;
        OnChangeScoreMultipliyer?.Invoke(multipliyerValue);
    }

    public void AddScoreMultiply(IScoreMultiplyProvider scoreMultiplyProvider)
    {
        scoreMultiplies.Add(scoreMultiplyProvider);

        scoreMultiplyProvider.OnApplyScoreMultiply += ApplyScore;
    }

    public void RemoveScoreMultiply(IScoreMultiplyProvider scoreMultiplyProvider)
    {
        scoreMultiplies.Remove(scoreMultiplyProvider);

        scoreMultiplyProvider.OnApplyScoreMultiply -= ApplyScore;
    }

    private void ApplyScore(IScoreMultiply scoreMultiply)
    {
        multipliyerValue = scoreMultiply.ApplyMultiply(multipliyerValue);
        if (multipliyerValue < 0.5f) multipliyerValue = 0.5f;

        multipliyerValue = Mathf.Round(multipliyerValue * 10f) / 10f;

        OnChangeScoreMultipliyer?.Invoke(multipliyerValue);
    }
}
