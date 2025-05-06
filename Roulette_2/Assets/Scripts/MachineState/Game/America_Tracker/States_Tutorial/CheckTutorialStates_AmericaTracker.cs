using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTutorialStates_AmericaTracker : IState
{
    private IGlobalStateMachineProvider _machineProvider;
    private ITutorialProgressProvider_Read _tutorialProgressProvider;

    public CheckTutorialStates_AmericaTracker(IGlobalStateMachineProvider machineProvider, ITutorialProgressProvider_Read tutorialProgressProvider)
    {
        _machineProvider = machineProvider;
        _tutorialProgressProvider = tutorialProgressProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - CHECK TUTORIAL STATE / AMERICA TRACKER</color>");

        if (_tutorialProgressProvider.HasPlayedTutorialById(6))
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
        _machineProvider.SetState(_machineProvider.GetState<Tutorial_01_AmericaTrackerIntroState_AmericaTracker>());
    }

    private void ChangeStateToMain()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_AmericaTracker>());
    }
}
