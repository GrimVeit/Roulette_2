using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRocketMoveModel
{
    private readonly List<IObstacleRocketControlProvider> obstacleRocketControlProviders = new List<IObstacleRocketControlProvider>();

    private readonly IRocketMoveProvider _rocketMoveProvider;
    private readonly PathData _pathData;

    public ObstacleRocketMoveModel(IRocketMoveProvider rocketMoveProvider, PathData pathData)
    {
        _rocketMoveProvider = rocketMoveProvider;
        _pathData = pathData;
    }

    public void AddObstacleRocketControlProvider(IObstacleRocketControlProvider provider)
    {
        obstacleRocketControlProviders.Add(provider);

        provider.OnApplyObstacleRocketControl += ApplyRocketMove;
    }

    public void RemoveObstacleRocketControlProvider(IObstacleRocketControlProvider provider)
    {
        obstacleRocketControlProviders.Add(provider);

        provider.OnApplyObstacleRocketControl -= ApplyRocketMove;
    }

    public void Clear()
    {
        if (obstacleRocketControlProviders.Count > 0)
        {
            for (int i = 0; i < obstacleRocketControlProviders.Count; i++)
            {
                obstacleRocketControlProviders[i].OnApplyObstacleRocketControl -= ApplyRocketMove;
            }

            obstacleRocketControlProviders.Clear();
        }
    }

    private void ApplyRocketMove(ObstacleType obstacleType, PathZone pathZone, IObstacleKnockProvider knockProvider)
    {
        var probabilityData =  _pathData.GetRandomProbabilityData(obstacleType, pathZone);

        var step = probabilityData.GetRandomStep();

        switch (step)
        {
            case Step.Left2:
                _rocketMoveProvider.MoveLeftDouble();
                if (obstacleType == ObstacleType.Minus)
                    knockProvider.KnockRight();

                return;
            case Step.Left1:
                _rocketMoveProvider.MoveLeft();
                if (obstacleType == ObstacleType.Minus)
                    knockProvider.KnockRight();
                return;
            case Step.Stay:
                if (obstacleType == ObstacleType.Minus)
                    knockProvider.KnockRight();
                return;
            case Step.Right1:
                _rocketMoveProvider.MoveRight();
                if (obstacleType == ObstacleType.Minus)
                    knockProvider.KnockLeft();
                return;
            case Step.Right2:
                _rocketMoveProvider.MoveRightDouble();
                if (obstacleType == ObstacleType.Minus)
                    knockProvider.KnockLeft();
                return;

        }
    }
}
