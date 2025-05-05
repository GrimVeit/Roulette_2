using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_03_ChanceExplanationState_America : IState
{
    private readonly IGlobalStateMachineProvider _stateMachine;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly IHandPointerProvider _handPointerProvider;
    private readonly IHighlightProvider _highlightProvider;
    private readonly IGameProgressProvider_Write _gameProgressProvider_Write;
    private readonly ITutorialProgressProvider_Write _tutorialProgressProvider_Write; 

    private IEnumerator timerCoroutine;

    public Tutorial_03_ChanceExplanationState_America(IGlobalStateMachineProvider stateMachine, DialoguePresenter dialoguePresenter, IHandPointerProvider handPointerProvider, IHighlightProvider highlightProvider, IGameProgressProvider_Write gameProgressProvider_Write, ITutorialProgressProvider_Write tutorialProgressProvider_Write)
    {
        _stateMachine = stateMachine;
        _dialoguePresenter = dialoguePresenter;
        _handPointerProvider = handPointerProvider;
        _highlightProvider = highlightProvider;
        _gameProgressProvider_Write = gameProgressProvider_Write;
        _tutorialProgressProvider_Write = tutorialProgressProvider_Write;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 02 STATE / AMERICA</color>");

        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);

        timerCoroutine = Timer(3);
        Coroutines.Start(timerCoroutine);


        _dialoguePresenter.Next();
    }

    public void ExitState()
    {
        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);
    }

    private IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        _dialoguePresenter.Deactivate();
        _handPointerProvider.Deactivate();
        _highlightProvider.DeselectAll();

        _gameProgressProvider_Write.OpenGame(4);
        _tutorialProgressProvider_Write.CompleteTutorial(3);

        ChangeStateToMain();
    }

    private void ChangeStateToMain()
    {
        _stateMachine.SetState(_stateMachine.GetState<MainState_America>());
    }
}
