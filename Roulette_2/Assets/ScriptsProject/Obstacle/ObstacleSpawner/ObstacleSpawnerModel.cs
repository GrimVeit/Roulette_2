using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawnerModel
{
    public event Action<int> OnSpawnObstacle;

    private IEnumerator coroutineSpawn;

    private readonly float minInterval;
    private readonly float maxInterval;

    private bool isActive = false;

    private int lastSpawnPointID = -1;

    private SpawnPointsData spawnPointsData;

    public ObstacleSpawnerModel(SpawnPointsData spawnPointsData, float minInterval, float maxInterval)
    {
        this.spawnPointsData = spawnPointsData;
        this.minInterval = minInterval;
        this.maxInterval = maxInterval;
    }

    public void ActivateSpawner()
    {
        isActive = true;

        if(coroutineSpawn != null) Coroutines.Stop(coroutineSpawn);

        coroutineSpawn = CoroutineSpawn();
        Coroutines.Start(coroutineSpawn);
    }

    public void DeactivateSpawner()
    {
        isActive = false;

        if (coroutineSpawn != null) Coroutines.Stop(coroutineSpawn);
    }

    private IEnumerator CoroutineSpawn()
    {
        while(isActive)
        {
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));

            var validPoints = spawnPointsData.points
            .Where(p => p.ID != lastSpawnPointID && p.ID != lastSpawnPointID - 1 && p.ID != lastSpawnPointID + 1)
            .ToList();

            

            if (validPoints.Count == 0)
            {
                Debug.LogWarning("Not found valid points");
            }

            var selectedPoint = spawnPointsData.GetRandomSpawnPointData(validPoints).ID;

            lastSpawnPointID = selectedPoint;

            OnSpawnObstacle?.Invoke(selectedPoint);
        }
    }
}
