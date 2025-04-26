using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BetGroup", menuName = "Game/Bet/New BetGroup")]
public class Bets : ScriptableObject
{
    public List<Bet> bets = new();
}
