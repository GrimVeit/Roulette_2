using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Strategy", menuName = "Game/Strategy/New Strategy")]
public class Strategy : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int chipCount;
    [SerializeField] private List<int> indexPositions = new List<int>();

    private StrategyData strategyData;

    public int ID => id;
    public Sprite Sprite => sprite;
    public int ChipCount => chipCount;
    public List<int> IndexPositions => indexPositions;

    public StrategyData StrategyData => strategyData;

    internal void SetData(StrategyData strategyData)
    {
        this.strategyData = strategyData;
    }
}
