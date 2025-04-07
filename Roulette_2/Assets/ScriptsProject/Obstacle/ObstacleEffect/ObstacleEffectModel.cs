using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleEffectModel
{
    private readonly IAnimationFrameProvider _animationFrameProvider;

    private readonly List<IObstacleEffectProvider> obstacleEffectProviders = new List<IObstacleEffectProvider>();

    public ObstacleEffectModel(IAnimationFrameProvider animationFrameProvider)
    {
        _animationFrameProvider = animationFrameProvider;
    }

    public void AddObstacleEffectProvider(IObstacleEffectProvider obstacleEffectProvider)
    {
        obstacleEffectProviders.Add(obstacleEffectProvider);

        obstacleEffectProvider.OnApplyObstacleEffect += ApplyObstacleEffect;
    }
    
    public void RemoveObstacleEffectProvider(IObstacleEffectProvider obstacleEffectProvider)
    {
        obstacleEffectProviders.Remove(obstacleEffectProvider);

        obstacleEffectProvider.OnApplyObstacleEffect -= ApplyObstacleEffect;
    }

    public void Clear()
    {
        if (obstacleEffectProviders.Count > 0)
        {
            for (int i = 0; i < obstacleEffectProviders.Count; i++)
            {
                obstacleEffectProviders[i].OnApplyObstacleEffect -= ApplyObstacleEffect;
            }

            obstacleEffectProviders.Clear();
        }
    }

    private void ApplyObstacleEffect(string id, Transform target)
    {
        _animationFrameProvider.ActivateAnimation(id, target, 1);
    }
}
