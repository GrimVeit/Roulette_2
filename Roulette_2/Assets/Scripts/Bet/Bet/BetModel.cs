using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetModel
{
    private readonly Bets _bets;
    private readonly List<RouletteNumber> rouletteNumbers = new();

    //private List<>

    //public BetModel(Bets bets)
    //{
    //    _bets = bets;
    //}

    public void SetRouletteNumber(RouletteNumber rouletteNumber)
    {
        rouletteNumbers.Add(rouletteNumber);
    }


}
