using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState_Mini : IState
{
    private readonly IGlobalStateMachineProvider _stateProvider;
    private readonly UIGameSceneRoot_Game _sceneRoot;

    public MainState_Mini(IGlobalStateMachineProvider stateProvider, UIGameSceneRoot_Game sceneRoot)
    {
        _sceneRoot = sceneRoot;
        _stateProvider = stateProvider;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - MAIN");

        _sceneRoot.OnClickToSpin += ChangeStateToRoulette;

        _sceneRoot.OpenFooterPanel();
        _sceneRoot.OpenHeaderPanel();
        _sceneRoot.OpenMainPanel();
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
        _stateProvider.SetState(_stateProvider.GetState<RouletteState_Mini>());
    }
}
