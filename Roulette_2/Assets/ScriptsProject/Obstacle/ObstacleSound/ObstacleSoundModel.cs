using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSoundModel
{
    private readonly List<IObstacleSoundProvider> obstacleSoundProviders = new List<IObstacleSoundProvider>();

    private readonly ISoundProvider _soundProvider;

    public ObstacleSoundModel(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
    }

    public void AddObstacleSoundProvider(IObstacleSoundProvider obstacleSoundProvider)
    {
        obstacleSoundProviders.Add(obstacleSoundProvider);

        obstacleSoundProvider.OnApplyObstacleSound += ApplyObstacleSound;
    }

    public void RemoveObstacleSoundProvider(IObstacleSoundProvider obstacleSoundProvider)
    {
        obstacleSoundProviders.Remove(obstacleSoundProvider);

        obstacleSoundProvider.OnApplyObstacleSound -= ApplyObstacleSound;
    }

    public void Clear()
    {
        if (obstacleSoundProviders.Count > 0)
        {
            for (int i = 0; i < obstacleSoundProviders.Count; i++)
            {
                obstacleSoundProviders[i].OnApplyObstacleSound -= ApplyObstacleSound;
            }

            obstacleSoundProviders.Clear();
        }
    }

    private void ApplyObstacleSound(string id)
    {
        _soundProvider.PlayOneShot(id);
    }
}
