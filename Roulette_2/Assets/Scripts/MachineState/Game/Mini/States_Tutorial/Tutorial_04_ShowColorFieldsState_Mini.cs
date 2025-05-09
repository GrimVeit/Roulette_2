using System.Collections;
using UnityEngine;

public class Tutorial_04_ShowColorFieldsState_Mini : IState
{
    private readonly IGlobalStateMachineProvider _stateMachine;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly IHighlightProvider _highlightProvider;
    private readonly IHandPointerProvider _handPointerProvider;

    private IEnumerator timerCoroutine;

    public Tutorial_04_ShowColorFieldsState_Mini(IGlobalStateMachineProvider stateMachine, DialoguePresenter dialoguePresenter, IHighlightProvider highlightProvider, IHandPointerProvider handPointerProvider)
    {
        _stateMachine = stateMachine;
        _dialoguePresenter = dialoguePresenter;
        _highlightProvider = highlightProvider;
        _handPointerProvider = handPointerProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 04 STATE / MINI</color>");

        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);

        timerCoroutine = Timer(2);
        Coroutines.Start(timerCoroutine);


        _dialoguePresenter.Next();
        _highlightProvider.Select(1);
        _handPointerProvider.Move(1);
    }

    public void ExitState()
    {
        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);

        _highlightProvider.Deselect(1);
    }

    private IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ChangeStateTo05();
    }

    private void ChangeStateTo05()
    {
        _stateMachine.SetState(_stateMachine.GetState<Tutorial_05_ShowChipsState_Mini>());
    }
}
