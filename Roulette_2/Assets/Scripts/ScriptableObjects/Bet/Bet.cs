using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bet
{
    public int ID => id;

    [SerializeField] private int id;
    [SerializeField] private List<int> numbers = new List<int>();
    [SerializeField] private float multiplyPayout;

    public List<int> Numbers => numbers;
    public float MultiplyPayout => multiplyPayout;
}
