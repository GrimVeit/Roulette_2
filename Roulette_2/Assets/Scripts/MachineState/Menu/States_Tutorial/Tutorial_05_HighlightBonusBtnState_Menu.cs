using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_05_HighlightBonusBtnState_Menu : IState
{
    private readonly IGlobalStateMachineProvider _globalStateMachineProvider;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly UIMainMenuRoot _sceneRoot;
    private readonly IHandPointerProvider _handPointerProvider;

    public Tutorial_05_HighlightBonusBtnState_Menu(IGlobalStateMachineProvider globalStateMachineProvider, DialoguePresenter dialoguePresenter, UIMainMenuRoot sceneRoot, IHandPointerProvider handPointerProvider)
    {
        _globalStateMachineProvider = globalStateMachineProvider;
        _dialoguePresenter = dialoguePresenter;
        _sceneRoot = sceneRoot;
        _handPointerProvider = handPointerProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 05 STATE / MENU</color>");

        _sceneRoot.OnClickToDailyReward_Main += ChangeStateTo_06;

        _dialoguePresenter.Next();
        _handPointerProvider.Move(1);
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToDailyReward_Main -= ChangeStateTo_06;
    }

    private void ChangeStateTo_06()
    {
        _globalStateMachineProvider.SetState(_globalStateMachineProvider.GetState<Tutorial_06_DailyBonus1State_Menu>());
    }
}
