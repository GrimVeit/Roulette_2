using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "BetGroup", menuName = "Game/Bet/New BetGroup")]
public class Bets : ScriptableObject
{
    public List<Bet> bets = new();

    public Bet GetBetById(int id)
    {
        return bets.FirstOrDefault(data => data.ID == id);
    }
}
