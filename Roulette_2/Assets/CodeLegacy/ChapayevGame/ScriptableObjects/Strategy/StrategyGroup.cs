using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "StrategyGroup", menuName = "Game/Strategy/New Strategy Group")]
public class StrategyGroup : ScriptableObject
{
    public List<Strategy> Strategies = new();

    public Strategy GetStrategyById(int id)
    {
        return Strategies.FirstOrDefault(data => data.ID == id);
    }

    public Strategy GetStrategyByChipCount(int count)
    {
        var list = Strategies.Where(data => data.ChipCount == count).ToList();

        return list[Random.Range(0, list.Count())];


    }

    public bool IsAvailableStrategy()
    {
        return Strategies.FirstOrDefault(data => data.StrategyData.IsOpen == false) != null;
    }

    public Strategy GetRandomCloseStrategy()
    {
        var strategies = Strategies.Where(data => !data.StrategyData.IsOpen).ToList();
        return strategies[Random.Range(0, strategies.Count)];
    }
}
