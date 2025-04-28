using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyRewardValues", menuName = "Game/DailyReward/Values")]
public class DailyRewardValues : ScriptableObject
{
    [SerializeField] private List<DailyRewardValue> dailyRewardValues = new List<DailyRewardValue>();

    public int GetRewardValueFromDay(int day)
    {
        return dailyRewardValues.FirstOrDefault(dr => dr.Day == day).Reward;
    }

    public int GetCountDays()
    {
        return dailyRewardValues.Count;
    }
}

[System.Serializable]
public class DailyRewardValue
{
    [SerializeField] private int day;
    [SerializeField] private int reward;
    
    public int Day => day;
    public int Reward => reward;
}
