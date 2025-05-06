using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_03_OneTwoBetState_French : IState
{
    private readonly IGlobalStateMachineProvider _stateMachine;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly IHighlightProvider _highlightProvider;
    private readonly IHandPointerProvider _handPointerProvider;

    private IEnumerator timerCoroutine;

    public Tutorial_03_OneTwoBetState_French(IGlobalStateMachineProvider stateMachine, DialoguePresenter dialoguePresenter, IHighlightProvider highlightProvider, IHandPointerProvider handPointerProvider)
    {
        _stateMachine = stateMachine;
        _dialoguePresenter = dialoguePresenter;
        _highlightProvider = highlightProvider;
        _handPointerProvider = handPointerProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 03 STATE / FRENCH</color>");

        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);

        timerCoroutine = Timer(3);
        Coroutines.Start(timerCoroutine);


        _dialoguePresenter.Next();
        _highlightProvider.Select(1);
        _handPointerProvider.Move(1);
    }

    public void ExitState()
    {
        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);
    }

    private IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ChangeStateTo04();
    }

    private void ChangeStateTo04()
    {
        _stateMachine.SetState(_stateMachine.GetState<Tutorial_04_LaPartageState_French>());
    }
}
