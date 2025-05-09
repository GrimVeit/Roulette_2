using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_06_EvenOddBetState_Euro : IState
{
    private readonly IGlobalStateMachineProvider _stateMachine;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly IHighlightProvider _highlightProvider;
    private readonly IHandPointerProvider _handPointerProvider;

    private IEnumerator timerCoroutine;

    public Tutorial_06_EvenOddBetState_Euro(IGlobalStateMachineProvider stateMachine, DialoguePresenter dialoguePresenter, IHighlightProvider highlightProvider, IHandPointerProvider handPointerProvider)
    {
        _stateMachine = stateMachine;
        _dialoguePresenter = dialoguePresenter;
        _highlightProvider = highlightProvider;
        _handPointerProvider = handPointerProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 06 STATE / EURO</color>");

        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);

        timerCoroutine = Timer(2);
        Coroutines.Start(timerCoroutine);

        _dialoguePresenter.Next();
        _highlightProvider.Select(4);
        _handPointerProvider.Move(4);
    }

    public void ExitState()
    {
        _highlightProvider.Deselect(4);

        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);
    }

    private IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ChangeStateTo07();
    }

    private void ChangeStateTo07()
    {
        _stateMachine.SetState(_stateMachine.GetState<Tutorial_07_RowColumnBetState_Euro>());
    }
}
