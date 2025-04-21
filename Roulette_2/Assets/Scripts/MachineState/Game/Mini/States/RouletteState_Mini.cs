using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteState_Mini : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameSceneRoot_Game _sceneRoot;
    private readonly RoulettePresenter _roulettePresenter;
    private readonly RouletteBallPresenter _rouletteBallPresenter;
    private readonly RouletteValueHistoryPresenter _rouletteValueHistoryPresenter;

    public RouletteState_Mini(IGlobalStateMachineProvider machineProvider,  UIGameSceneRoot_Game sceneRoot, RoulettePresenter roulettePresenter, RouletteBallPresenter rouletteBallPresenter, RouletteValueHistoryPresenter rouletteValueHistoryPresenter)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _roulettePresenter = roulettePresenter;
        _rouletteBallPresenter = rouletteBallPresenter;
        _rouletteValueHistoryPresenter = rouletteValueHistoryPresenter;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - ROULETTE");

        _rouletteBallPresenter.OnBallStopped += _roulettePresenter.RollBallToSlot;
        _roulettePresenter.OnStopSpin += ChangeStateToResult;

        _rouletteValueHistoryPresenter.ClearAll();
        _sceneRoot.OpenRoulettePanel();
        _roulettePresenter.StartSpin();
        _rouletteBallPresenter.StartSpin();
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - ROULETTE");

        _rouletteBallPresenter.OnBallStopped -= _roulettePresenter.RollBallToSlot;
        _roulettePresenter.OnStopSpin -= ChangeStateToResult;
    }

    private void ChangeStateToResult()
    {
        _machineProvider.SetState(_machineProvider.GetState<ResultState_Mini>());
    }
}
