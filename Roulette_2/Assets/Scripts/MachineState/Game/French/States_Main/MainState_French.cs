using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState_French : IState
{
    private readonly IGlobalStateMachineProvider _stateProvider;
    private readonly UIGameSceneRoot_Game _sceneRoot;
    private readonly BetPresenter _betPresenter;
    private readonly IBetCellActivatorProvider _betCellActivatorProvider;
    private readonly IPseudoChipActivatorProvider _pseudoChipActivatorProvider;
    
    public MainState_French(IGlobalStateMachineProvider stateProvider, UIGameSceneRoot_Game sceneRoot, BetPresenter betPresenter, IBetCellActivatorProvider betCellActivatorProvider, IPseudoChipActivatorProvider pseudoChipActivatorProvider)
    {
        _sceneRoot = sceneRoot;
        _stateProvider = stateProvider;
        _betPresenter = betPresenter;
        _betCellActivatorProvider = betCellActivatorProvider;
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
        _betCellActivatorProvider.Activate();
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - MAIN");

        _sceneRoot.OnClickToSpin -= ChangeStateToRoulette;

        _sceneRoot.CloseFooterPanel();
        _sceneRoot.CloseMenuPanel();
        _sceneRoot.CloseBalancePanel();

        _betCellActivatorProvider.Deactivate();
        _pseudoChipActivatorProvider.Deactivate();
    }

    private void ChangeStateToRoulette()
    {
        _stateProvider.SetState(_stateProvider.GetState<RouletteState_French>());
    }
}
