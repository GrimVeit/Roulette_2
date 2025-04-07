using UnityEngine;

[CreateAssetMenu(fileName = "SpawnPointData", menuName = "Game/Obstacle/NewSpawnPointData")]
public class SpawnPointData : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField][Range(0, 1)] private float chance;

    public int ID => id;
    public float Chance => chance;
}
