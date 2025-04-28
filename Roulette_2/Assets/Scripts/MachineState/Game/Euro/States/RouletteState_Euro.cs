using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteState_Euro : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameSceneRoot_Game _sceneRoot;
    private readonly RoulettePresenter _roulettePresenter;
    private readonly RouletteBallPresenter _rouletteBallPresenter;
    private readonly RouletteValueHistoryPresenter _valueHistoryPresenter;

    private readonly IMetric_GameCount _gameCountMetric;
    private readonly IMetric_GameTypeCount _gameTypeCountMetric;

    public RouletteState_Euro(IGlobalStateMachineProvider machineProvider, UIGameSceneRoot_Game sceneRoot, RoulettePresenter roulettePresenter, RouletteBallPresenter rouletteBallPresenter, RouletteValueHistoryPresenter valueHistoryPresenter, IMetric_GameCount gameCount, IMetric_GameTypeCount typeCount)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _roulettePresenter = roulettePresenter;
        _rouletteBallPresenter = rouletteBallPresenter;
        _valueHistoryPresenter = valueHistoryPresenter;
        _gameCountMetric = gameCount;
        _gameTypeCountMetric = typeCount;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - ROULETTE");

        _rouletteBallPresenter.OnBallStopped += _roulettePresenter.RollBallToSlot;
        _roulettePresenter.OnStopSpin += ChangeStateToResult;

        _valueHistoryPresenter.ClearAll();
        _sceneRoot.OpenRoulettePanel();
        _roulettePresenter.StartSpin();
        _rouletteBallPresenter.StartSpin();

        _gameCountMetric.AddGame();
        _gameTypeCountMetric.AddGameType(2);
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - ROULETTE");

        _rouletteBallPresenter.OnBallStopped -= _roulettePresenter.RollBallToSlot;
        _roulettePresenter.OnStopSpin -= ChangeStateToResult;
    }

    private void ChangeStateToResult()
    {
        _machineProvider.SetState(_machineProvider.GetState<ResultState_Euro>());
    }
}
