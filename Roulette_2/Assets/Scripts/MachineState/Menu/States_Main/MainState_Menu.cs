using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState_Menu : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;

    public MainState_Menu(IGlobalStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - MAIN STATE / MENU</color>");

        _sceneRoot.OnClickToDailyReward_Main += ChangeStateToDailyReward;
        _sceneRoot.OnClickToLeaderboard += ChangeStateToLeaderboard;
        _sceneRoot.OnClickToTasks_Main += ChangeStateToDailyTasks;
        _sceneRoot.OnClickToChips_Main += ChangeStateToChipStore;

        _sceneRoot.OpenMainPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToDailyReward_Main -= ChangeStateToDailyReward;
        _sceneRoot.OnClickToLeaderboard -= ChangeStateToLeaderboard;
        _sceneRoot.OnClickToTasks_Main -= ChangeStateToDailyTasks;
        _sceneRoot.OnClickToChips_Main -= ChangeStateToChipStore;
    }

    private void ChangeStateToDailyTasks()
    {
        _machineProvider.SetState(_machineProvider.GetState<DailyTasksState_Menu>());
    }

    private void ChangeStateToLeaderboard()
    {
        _machineProvider.SetState(_machineProvider.GetState<LeaderboardState_Menu>());
    }

    private void ChangeStateToDailyReward()
    {
        _machineProvider.SetState(_machineProvider.GetState<DailyRewardState_Menu>());
    }

    private void ChangeStateToChipStore()
    {
        _machineProvider.SetState(_machineProvider.GetState<ChipStoreState_Menu>());
    }
}
