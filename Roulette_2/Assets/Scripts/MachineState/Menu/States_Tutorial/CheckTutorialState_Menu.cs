using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTutorialState_Menu : IState
{

    private IGlobalStateMachineProvider _machineProvider;
    private ITutorialProgressProvider_Read _tutorialProgressProvider;

    public CheckTutorialState_Menu(IGlobalStateMachineProvider machineProvider, ITutorialProgressProvider_Read tutorialProgressProvider)
    {
        _machineProvider = machineProvider;
        _tutorialProgressProvider = tutorialProgressProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - CHECK TUTORIAL STATE / MENU</color>");

        if (_tutorialProgressProvider.HasPlayedTutorialById(0))
        {
            ChangeStateToMain();
        }
        else
        {
            ChangeStateToStartTutorial();
        }
    }

    public void ExitState()
    {

    }

    private void ChangeStateToStartTutorial()
    {
        _machineProvider.SetState(_machineProvider.GetState<Tutorial_01_IntroGreetingState_Menu>());
    }

    private void ChangeStateToMain()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_Menu>());
    }
}
