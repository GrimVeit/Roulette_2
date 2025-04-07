using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObstacleSpawnChance
{
    [SerializeField] private List<ObstacleSpawn> obstacleSpawns = new List<ObstacleSpawn>();

    public Obstacle GetRandomObstacle()
    {
        int totalSum = 0;
        foreach (var obstacleSpawn in obstacleSpawns)
            totalSum += obstacleSpawn.chanceDrop;

        if (totalSum == 0) return null;

        int randomValue = Random.Range(0, totalSum);
        int sum = 0;

        foreach (var obstacleSpawn in obstacleSpawns)
        {
            sum += obstacleSpawn.chanceDrop;

            if (randomValue <= sum)
                return obstacleSpawn.obstaclePrefab;
        }

        return null;
    }
}
