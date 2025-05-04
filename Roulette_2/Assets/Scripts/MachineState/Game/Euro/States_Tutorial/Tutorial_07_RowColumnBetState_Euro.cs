using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_07_RowColumnBetState_Euro : IState
{
    private readonly IGlobalStateMachineProvider _stateMachine;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly IHighlightProvider _highlightProvider;

    private IEnumerator timerCoroutine;

    public Tutorial_07_RowColumnBetState_Euro(IGlobalStateMachineProvider stateMachine, DialoguePresenter dialoguePresenter, IHighlightProvider highlightProvider)
    {
        _stateMachine = stateMachine;
        _dialoguePresenter = dialoguePresenter;
        _highlightProvider = highlightProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 07 STATE / EURO</color>");

        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);

        timerCoroutine = Timer(3);
        Coroutines.Start(timerCoroutine);

        _dialoguePresenter.Next();
        _highlightProvider.Select(5);
    }

    public void ExitState()
    {
        _highlightProvider.DeselectAll();

        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);
    }

    private IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ChangeStateTo08();
    }

    private void ChangeStateTo08()
    {
        _stateMachine.SetState(_stateMachine.GetState<Tutorial_08_SummaryMonicaState_Euro>());
    }
}
