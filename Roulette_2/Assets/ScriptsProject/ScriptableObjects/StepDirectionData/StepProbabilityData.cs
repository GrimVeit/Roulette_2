using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StepProbability", menuName = "Game/Step/NewStepProbability")]
public class StepProbabilityData : ScriptableObject
{
    public PathZone PathZone => pathZone;
    public ObstacleType ObstacleType => obstacleType;

    [SerializeField] private List<StepProbability> stepProbabilities = new List<StepProbability>();
    [SerializeField] private PathZone pathZone;
    [SerializeField] private ObstacleType obstacleType;

    public Step GetRandomStep()
    {
        int totalSum = 0;
        foreach (var step in stepProbabilities)
            totalSum += step.probability;

        if(totalSum == 0) return Step.Stay;

        int randomValue = Random.Range(0, totalSum);
        int sum = 0;

        foreach(var stepProbability in stepProbabilities)
        {
            sum += stepProbability.probability;

            if(randomValue <= sum)
                return stepProbability.step;
        }

        return Step.Stay;
    }
}

public enum PathZone
{
    Left, Right, Center
}

public enum ObstacleType
{
    Minus, Plus
}
