using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultState_French : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameSceneRoot_Game _sceneRoot;

    public ResultState_French(IGlobalStateMachineProvider machineProvider, UIGameSceneRoot_Game sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - RESULT");

        ChangeStateToMain();
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - RESULT");
    }

    private void ChangeStateToMain()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_French>());
    }
}
