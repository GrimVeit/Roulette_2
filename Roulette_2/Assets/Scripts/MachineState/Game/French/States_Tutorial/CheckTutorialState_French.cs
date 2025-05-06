using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTutorialState_French : IState
{
    private IGlobalStateMachineProvider _machineProvider;
    private ITutorialProgressProvider_Read _tutorialProgressProvider;

    public CheckTutorialState_French(IGlobalStateMachineProvider machineProvider, ITutorialProgressProvider_Read tutorialProgressProvider)
    {
        _machineProvider = machineProvider;
        _tutorialProgressProvider = tutorialProgressProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - CHECK TUTORIAL STATE / AMERICA</color>");

        if (_tutorialProgressProvider.HasPlayedTutorialById(5))
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
        _machineProvider.SetState(_machineProvider.GetState<Tutorial_01_IntroFrenchState_French>());
    }

    private void ChangeStateToMain()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_French>());
    }
}
