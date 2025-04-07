using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "WinPrice", menuName = "Game/Win/WinPrice")]
public class WinPrices : ScriptableObject
{
    public List<WinPrice> Prices = new List<WinPrice>();

    public WinPrice GetWinPriceByChipCount(int chipCount)
    {
        int findIndex = chipCount;

        if(findIndex > 3)
            findIndex = 3;

        return Prices.FirstOrDefault(data => data.CountChip == findIndex);
    }
}

[Serializable]
public class WinPrice
{
    [SerializeField] private int countChip;
    [SerializeField] private int win;

    public int CountChip => countChip;
    public int Win => win;
}
