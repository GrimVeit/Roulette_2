using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_08_ClickSpinState_Mini : IState
{
    private readonly IGlobalStateMachineProvider _stateMachine;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly UIGameSceneRoot_Game _sceneRoot;
    private readonly IHandPointerProvider _handPointerProvider;

    public Tutorial_08_ClickSpinState_Mini(IGlobalStateMachineProvider stateMachine, DialoguePresenter dialoguePresenter, UIGameSceneRoot_Game sceneRoot, IHandPointerProvider handPointerProvider)
    {
        _stateMachine = stateMachine;
        _dialoguePresenter = dialoguePresenter;
        _sceneRoot = sceneRoot;
        _handPointerProvider = handPointerProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 08 STATE / MINI</color>");

        _sceneRoot.OnClickToSpin += ChangeStateTo09;

        _dialoguePresenter.Next();
        _handPointerProvider.Activate();
        _handPointerProvider.Move(3);
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToSpin -= ChangeStateTo09;
    }

    private void ChangeStateTo09()
    {
        _dialoguePresenter.Deactivate();
        _handPointerProvider.Deactivate();

        _stateMachine.SetState(_stateMachine.GetState<Tutorial_09_RouletteSpinState_Mini>());
    }
}
