using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_08_HighlightTasksBtnState_Menu : IState
{
    private readonly IGlobalStateMachineProvider _globalStateMachineProvider;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly UIMainMenuRoot _sceneRoot;
    private readonly IHandPointerProvider _handPointerProvider;

    public Tutorial_08_HighlightTasksBtnState_Menu(IGlobalStateMachineProvider globalStateMachineProvider, DialoguePresenter dialoguePresenter, UIMainMenuRoot sceneRoot, IHandPointerProvider handPointerProvider)
    {
        _globalStateMachineProvider = globalStateMachineProvider;
        _dialoguePresenter = dialoguePresenter;
        _sceneRoot = sceneRoot;
        _handPointerProvider = handPointerProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 05 STATE / MENU</color>");

        _sceneRoot.OnClickToTasks_Main += ChangeStateTo_09;
        _sceneRoot.OpenMainPanel();

        _dialoguePresenter.Next();
        _handPointerProvider.Move(4);
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToTasks_Main -= ChangeStateTo_09;
    }

    private void ChangeStateTo_09()
    {
        _handPointerProvider.Deactivate();
        _globalStateMachineProvider.SetState(_globalStateMachineProvider.GetState<Tutorial_09_DailyTasksState_Menu>());
    }
}
