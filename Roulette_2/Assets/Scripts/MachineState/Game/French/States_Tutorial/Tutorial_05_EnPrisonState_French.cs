using System.Collections;
using UnityEngine;

public class Tutorial_05_EnPrisonState_French : IState
{
    private readonly IGlobalStateMachineProvider _stateMachine;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly IHighlightProvider _highlightProvider;
    private readonly IHandPointerProvider _handPointerProvider;

    private IEnumerator timerCoroutine;

    public Tutorial_05_EnPrisonState_French(IGlobalStateMachineProvider stateMachine, DialoguePresenter dialoguePresenter, IHighlightProvider highlightProvider, IHandPointerProvider handPointerProvider)
    {
        _stateMachine = stateMachine;
        _dialoguePresenter = dialoguePresenter;
        _highlightProvider = highlightProvider;
        _handPointerProvider = handPointerProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 05 STATE / FRENCH</color>");

        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);

        timerCoroutine = Timer(4);
        Coroutines.Start(timerCoroutine);


        _dialoguePresenter.Next();
        _highlightProvider.Select(2);
        _handPointerProvider.Move(3);
    }

    public void ExitState()
    {
        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);
    }

    private IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        _highlightProvider.DeselectAll();
        _handPointerProvider.Deactivate();

        ChangeStateTo06();
    }

    private void ChangeStateTo06()
    {
        _stateMachine.SetState(_stateMachine.GetState<Tutorial_06_ChanceExplanationState_French>());
    }
}
