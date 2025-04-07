using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "PathData", menuName = "Game/Step/NewPathData")]
public class PathData : ScriptableObject 
{
    [SerializeField] private List<StepProbabilityData> datas = new List<StepProbabilityData>();

    public StepProbabilityData GetRandomProbabilityData(ObstacleType type, PathZone pathZone)
    {
        var data = datas.Where(p => p.ObstacleType == type &&  p.PathZone == pathZone).ToArray();

        if(data.Length == 0)
        {
            Debug.LogWarning($"No data for zone {pathZone} with type obstacle {type}");
            return null;
        }

        int randomIndex = Random.Range(0, data.Length);
        return data[randomIndex];
    }
}
