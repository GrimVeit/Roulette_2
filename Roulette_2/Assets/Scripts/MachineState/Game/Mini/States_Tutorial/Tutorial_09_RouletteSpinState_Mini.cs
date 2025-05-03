using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_09_RouletteSpinState_Mini : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameSceneRoot_Game _sceneRoot;
    private readonly RoulettePresenter _roulettePresenter;
    private readonly RouletteBallPresenter _rouletteBallPresenter;
    private readonly RouletteValueHistoryPresenter _rouletteValueHistoryPresenter;

    private readonly IMetric_GameCount _gameCountMetric;
    private readonly IMetric_GameTypeCount _gameTypeCountMetric;

    public Tutorial_09_RouletteSpinState_Mini(IGlobalStateMachineProvider machineProvider, UIGameSceneRoot_Game sceneRoot, RoulettePresenter roulettePresenter, RouletteBallPresenter rouletteBallPresenter, RouletteValueHistoryPresenter rouletteValueHistoryPresenter, IMetric_GameCount gameCountMetric, IMetric_GameTypeCount gameTypeCount)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _roulettePresenter = roulettePresenter;
        _rouletteBallPresenter = rouletteBallPresenter;
        _rouletteValueHistoryPresenter = rouletteValueHistoryPresenter;
        _gameCountMetric = gameCountMetric;
        _gameTypeCountMetric = gameTypeCount;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 09 STATE / MINI</color>");

        _rouletteBallPresenter.OnBallStopped += _roulettePresenter.RollBallToSlot;
        _roulettePresenter.OnStopSpin += ChangeStateTo10;

        _rouletteValueHistoryPresenter.ClearAll();
        _sceneRoot.CloseFooterPanel();
        _sceneRoot.OpenRoulettePanel();
        _roulettePresenter.StartSpin();
        _rouletteBallPresenter.StartSpin();

        _gameCountMetric.AddGame();
        _gameTypeCountMetric.AddGameType(1);
    }

    public void ExitState()
    {
        _rouletteBallPresenter.OnBallStopped -= _roulettePresenter.RollBallToSlot;
        _roulettePresenter.OnStopSpin -= ChangeStateTo10;
    }

    private void ChangeStateTo10()
    {
        _machineProvider.SetState(_machineProvider.GetState<Tutorial_10_ShowResultState_Mini>());
    }
}
