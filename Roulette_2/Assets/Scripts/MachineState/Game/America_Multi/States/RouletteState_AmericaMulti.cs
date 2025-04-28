using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteState_AmericaMulti : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameSceneRoot_Game _sceneRoot;
    private readonly List<RoulettePresenter> _roulettePresenters;
    private readonly List<RouletteBallPresenter> _rouletteBallPresenters;
    private readonly RouletteValueHistoryPresenter _rouletteValueHistoryPresenter;

    private readonly IMetric_GameCount _gameCountMetric;
    private readonly IMetric_GameTypeCount _gameTypeCountMetric;

    private int RouletteCount => _roulettePresenters.Count;
    private int _currentCount = 0;

    public RouletteState_AmericaMulti(IGlobalStateMachineProvider machineProvider, UIGameSceneRoot_Game sceneRoot, List<RoulettePresenter> roulettePresenters, List<RouletteBallPresenter> rouletteBallPresenters, RouletteValueHistoryPresenter rouletteValueHistoryPresenter, IMetric_GameCount gameCount, IMetric_GameTypeCount typeCount)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _roulettePresenters = roulettePresenters;
        _rouletteBallPresenters = rouletteBallPresenters;
        _rouletteValueHistoryPresenter = rouletteValueHistoryPresenter;

        _gameCountMetric = gameCount;
        _gameTypeCountMetric = typeCount;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - ROULETTE");

        for (int i = 0; i < RouletteCount; i++)
        {
            _rouletteBallPresenters[i].OnBallStopped += _roulettePresenters[i].RollBallToSlot;
        }

        _rouletteValueHistoryPresenter.ClearAll();

        _roulettePresenters.ForEach(rp => 
        {
            rp.OnStopSpin += CheckRoulette;
            rp.StartSpin();
        });

        _rouletteBallPresenters.ForEach(rp => rp.StartSpin());
        _sceneRoot.OpenRoulettePanel();

        _gameCountMetric.AddGame();
        _gameTypeCountMetric.AddGameType(4);
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - ROULETTE");

        for (int i = 0; i < RouletteCount; i++)
        {
            _rouletteBallPresenters[i].OnBallStopped -= _roulettePresenters[i].RollBallToSlot;
        }

        _roulettePresenters.ForEach(rp => rp.OnStopSpin -= CheckRoulette);
    }

    private void CheckRoulette()
    {
        _currentCount += 1;

        if(_currentCount >= RouletteCount)
        {
            _currentCount = 0;
            ChangeStateToResult();
        }
    }

    private void ChangeStateToResult()
    {
        _machineProvider.SetState(_machineProvider.GetState<ResultState_AmericaMulti>());
    }
}
