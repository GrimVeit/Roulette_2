using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultState_Mini : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameSceneRoot_Game _sceneRoot;
    private readonly BetPresenter _betPresenter;

    public ResultState_Mini(IGlobalStateMachineProvider machineProvider, UIGameSceneRoot_Game sceneRoot, BetPresenter betPresenter)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _betPresenter = betPresenter;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - RESULT");

        _betPresenter.SearchWin();

        ChangeStateToMain();
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - RESULT");
    }

    private void ChangeStateToMain()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_Mini>());
    }
}
