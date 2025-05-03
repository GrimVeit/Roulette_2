using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState_Mini : IState
{
    private readonly IGlobalStateMachineProvider _stateProvider;
    private readonly UIGameSceneRoot_Game _sceneRoot;
    private readonly BetPresenter _betPresenter;
    private readonly IPseudoChipActivatorProvider _pseudoChipActivatorProvider;
    private readonly IBetCellActivatorProvider _betCellActivatorProvider;

    public MainState_Mini(IGlobalStateMachineProvider stateProvider, UIGameSceneRoot_Game sceneRoot, BetPresenter betPresenter, IPseudoChipActivatorProvider pseudoChipActivatorProvider, IBetCellActivatorProvider betCellActivatorProvider)
    {
        _sceneRoot = sceneRoot;
        _stateProvider = stateProvider;
        _betPresenter = betPresenter;
        _pseudoChipActivatorProvider = pseudoChipActivatorProvider;
        _betCellActivatorProvider = betCellActivatorProvider;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - MAIN");

        _sceneRoot.OnClickToSpin += ChangeStateToRoulette;

        _sceneRoot.OpenFooterPanel();
        _sceneRoot.OpenBalancePanel();
        _sceneRoot.OpenMainPanel();
        _sceneRoot.OpenMenuPanel();

        _pseudoChipActivatorProvider.Activate();
        _betCellActivatorProvider.Activate();

        _betPresenter.ClearTable();
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - MAIN");

        _sceneRoot.OnClickToSpin -= ChangeStateToRoulette;

        _sceneRoot.CloseFooterPanel();
        _sceneRoot.CloseBalancePanel();
        _sceneRoot.CloseMenuPanel();

        _pseudoChipActivatorProvider.Deactivate();
        _betCellActivatorProvider.Deactivate();
    }

    private void ChangeStateToRoulette()
    {
        _stateProvider.SetState(_stateProvider.GetState<RouletteState_Mini>());
    }
}
