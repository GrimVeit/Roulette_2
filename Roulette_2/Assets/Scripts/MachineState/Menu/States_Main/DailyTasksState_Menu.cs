using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyTasksState_Menu : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;

    public DailyTasksState_Menu(IGlobalStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - DAILY TASKS STATE / MENU</color>");

        _sceneRoot.OnClickToBack_Tasks += ChangeStateToMain;

        _sceneRoot.OpenTasksPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToBack_Tasks -= ChangeStateToMain;
    }

    private void ChangeStateToMain()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_Menu>());
    }
}
