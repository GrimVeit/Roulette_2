using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_06_ExplainChipPlaceState_Mini : IState
{
    private readonly IGlobalStateMachineProvider _stateMachine;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly IHighlightProvider _highlightProvider;

    private IEnumerator timerCoroutine;

    public Tutorial_06_ExplainChipPlaceState_Mini(IGlobalStateMachineProvider stateMachine, DialoguePresenter dialoguePresenter, IHighlightProvider highlightProvider)
    {
        _stateMachine = stateMachine;
        _dialoguePresenter = dialoguePresenter;
        _highlightProvider = highlightProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 06 STATE / MINI</color>");

        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);

        timerCoroutine = Timer(3);
        Coroutines.Start(timerCoroutine);


        _dialoguePresenter.Next();
    }

    public void ExitState()
    {
        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);

        _highlightProvider.DeselectAll();
    }

    private IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ChangeStateTo07();
    }

    private void ChangeStateTo07()
    {
        _stateMachine.SetState(_stateMachine.GetState<Tutorial_07_WaitChipPlaceState_Mini>());
    }
}
