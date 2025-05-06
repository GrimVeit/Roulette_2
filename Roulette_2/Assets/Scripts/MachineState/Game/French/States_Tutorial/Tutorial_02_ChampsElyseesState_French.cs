using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_02_ChampsElyseesState_French : IState
{
    private readonly IGlobalStateMachineProvider _stateMachine;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly IHighlightProvider _highlightProvider;
    private readonly IHandPointerProvider _handPointerProvider;

    private IEnumerator timerCoroutine;

    public Tutorial_02_ChampsElyseesState_French(IGlobalStateMachineProvider stateMachine, DialoguePresenter dialoguePresenter, IHighlightProvider highlightProvider, IHandPointerProvider handPointerProvider)
    {
        _stateMachine = stateMachine;
        _dialoguePresenter = dialoguePresenter;
        _highlightProvider = highlightProvider;
        _handPointerProvider = handPointerProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 02 STATE / FRENCH</color>");

        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);

        timerCoroutine = Timer(3);
        Coroutines.Start(timerCoroutine);

        _dialoguePresenter.Next();
        _highlightProvider.Select(0);
        _handPointerProvider.Activate();
        _handPointerProvider.Move(0);
    }

    public void ExitState()
    {
        _highlightProvider.Deselect(0);

        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);
    }

    private IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ChangeStateTo03();
    }

    private void ChangeStateTo03()
    {
        _stateMachine.SetState(_stateMachine.GetState<Tutorial_03_OneTwoBetState_French>());
    }
}
