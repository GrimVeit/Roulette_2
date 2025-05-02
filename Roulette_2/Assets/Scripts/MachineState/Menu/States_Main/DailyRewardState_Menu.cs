using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewardState_Menu : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;

    public DailyRewardState_Menu(IGlobalStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - DAILY REWARD STATE / MENU</color>");

        _sceneRoot.OnClickToBack_DailyReward += ChangeStateToMain;

        _sceneRoot.OpenDailyRewardPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToBack_DailyReward -= ChangeStateToMain;
    }

    private void ChangeStateToMain()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_Menu>());
    }
}
