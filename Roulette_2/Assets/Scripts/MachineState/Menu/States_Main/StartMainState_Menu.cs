using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMainState_Menu : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly FirebaseDatabasePresenter _firebaseDatabasePresenter;

    public StartMainState_Menu(IGlobalStateMachineProvider machineProvider, FirebaseDatabasePresenter firebaseDatabasePresenter)
    {
        _machineProvider = machineProvider;
        _firebaseDatabasePresenter = firebaseDatabasePresenter;
    }

    public StartMainState_Menu(IGlobalStateMachineProvider machineProvider)
    {
        _machineProvider = machineProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - START MAIN STATE / MENU</color>");

        _firebaseDatabasePresenter.DisplayUsersRecords();

        ChangeStateToMain();
    }

    public void ExitState()
    {

    }

    private void ChangeStateToMain()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_Menu>());
    }

}
