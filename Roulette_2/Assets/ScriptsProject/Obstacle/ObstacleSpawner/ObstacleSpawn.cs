using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObstacleSpawn
{
    [Range(0, 100)]public int chanceDrop;
    public Obstacle obstaclePrefab;
}
