using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnPointData Group", menuName = "Game/Obstacle/NewSpawnPointData Group")]
public class SpawnPointsData : ScriptableObject
{
    public List<SpawnPointData> points = new List<SpawnPointData>();

    public SpawnPointData GetRandomSpawnPointData()
    {
        float totalChance = points.Sum(p => p.Chance);
        float randomValue = Random.Range(0, totalChance);
        float currentSum = 0f;

        for (int i = 0; i < points.Count; i++)
        {
            currentSum += points[i].Chance;
            if(randomValue <= currentSum) return points[i];
        }

        return null;
    }

    public SpawnPointData GetRandomSpawnPointData(List<SpawnPointData> spawnPoints)
    {
        float totalChance = spawnPoints.Sum(p => p.Chance);
        float randomValue = Random.Range(0, totalChance);
        float currentSum = 0f;

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            currentSum += spawnPoints[i].Chance;
            if (randomValue <= currentSum) return spawnPoints[i];
        }

        return null;
    }
}
