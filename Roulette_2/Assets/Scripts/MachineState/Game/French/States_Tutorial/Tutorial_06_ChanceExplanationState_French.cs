using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_06_ChanceExplanationState_French : IState
{
    private readonly IGlobalStateMachineProvider _stateMachine;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly IGameProgressProvider_Write _gameProgressProvider_Write;
    private readonly ITutorialProgressProvider_Write _tutorialProgressProvider_Write;

    private IEnumerator timerCoroutine;

    public Tutorial_06_ChanceExplanationState_French(IGlobalStateMachineProvider stateMachine, DialoguePresenter dialoguePresenter, IGameProgressProvider_Write gameProgressProvider_Write, ITutorialProgressProvider_Write tutorialProgressProvider_Write)
    {
        _stateMachine = stateMachine;
        _dialoguePresenter = dialoguePresenter;
        _gameProgressProvider_Write = gameProgressProvider_Write;
        _tutorialProgressProvider_Write = tutorialProgressProvider_Write;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 06 STATE / FRENCH</color>");

        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);

        timerCoroutine = Timer(4);
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

        _gameProgressProvider_Write.OpenGame(6);
        _tutorialProgressProvider_Write.CompleteTutorial(5);

        ChangeStateToMain();
    }

    private void ChangeStateToMain()
    {
        _stateMachine.SetState(_stateMachine.GetState<MainState_French>());
    }
}
