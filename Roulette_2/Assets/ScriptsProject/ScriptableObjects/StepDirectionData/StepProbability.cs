using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Step
{
    Left2 = -2,
    Left1 = -1,
    Stay = 0,
    Right1 = 1,
    Right2 = 2,
}

[System.Serializable]
public class StepProbability
{
    public Step step;
    [Range(0, 100)] public int probability;
}
