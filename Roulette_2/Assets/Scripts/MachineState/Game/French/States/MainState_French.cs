using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState_French : IState
{
    private readonly IGlobalStateMachineProvider _stateProvider;
    private readonly UIGameSceneRoot_Game _sceneRoot;
    private readonly BetPresenter _betPresenter;
    
    public MainState_French(IGlobalStateMachineProvider stateProvider, UIGameSceneRoot_Game sceneRoot, BetPresenter betPresenter)
    {
        _sceneRoot = sceneRoot;
        _stateProvider = stateProvider;
        _betPresenter = betPresenter;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - MAIN");

        _sceneRoot.OnClickToSpin += ChangeStateToRoulette;

        _sceneRoot.OpenFooterPanel();
        _sceneRoot.OpenHeaderPanel();
        _sceneRoot.OpenMainPanel();

        _betPresenter.ClearTable();
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - MAIN");

        _sceneRoot.OnClickToSpin -= ChangeStateToRoulette;

        _sceneRoot.CloseFooterPanel();
        _sceneRoot.CloseHeaderPanel();
    }

    private void ChangeStateToRoulette()
    {
        _stateProvider.SetState(_stateProvider.GetState<RouletteState_French>());
    }
}
