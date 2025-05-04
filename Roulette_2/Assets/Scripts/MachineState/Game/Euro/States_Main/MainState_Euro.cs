using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState_Euro : IState
{
    private readonly IGlobalStateMachineProvider _stateProvider;
    private readonly UIGameSceneRoot_Game _sceneRoot;
    private readonly BetPresenter _betPresenter;
    private readonly IBetCellActivatorProvider _cellActivatorProvider;
    private readonly IPseudoChipActivatorProvider _pseudoChipActivatorProvider;

    public MainState_Euro(IGlobalStateMachineProvider stateProvider, UIGameSceneRoot_Game sceneRoot, BetPresenter betPresenter, IBetCellActivatorProvider cellActivatorProvider, IPseudoChipActivatorProvider pseudoChipActivatorProvider)
    {
        _sceneRoot = sceneRoot;
        _stateProvider = stateProvider;
        _betPresenter = betPresenter;
        _cellActivatorProvider = cellActivatorProvider;
        _pseudoChipActivatorProvider = pseudoChipActivatorProvider;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - MAIN");

        _sceneRoot.OnClickToSpin += ChangeStateToRoulette;

        _sceneRoot.OpenFooterPanel();
        _sceneRoot.OpenBalancePanel();
        _sceneRoot.OpenMenuPanel();
        _sceneRoot.OpenMainPanel();

        _betPresenter.ClearTable();

        _pseudoChipActivatorProvider.Activate();
        _cellActivatorProvider.Activate();
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - MAIN");

        _sceneRoot.OnClickToSpin -= ChangeStateToRoulette;

        _sceneRoot.CloseFooterPanel();
        _sceneRoot.CloseBalancePanel();
        _sceneRoot.CloseMenuPanel();

        _pseudoChipActivatorProvider.Deactivate();
        _cellActivatorProvider.Deactivate();
    }

    private void ChangeStateToRoulette()
    {
        _stateProvider.SetState(_stateProvider.GetState<RouletteState_Euro>());
    }
}
