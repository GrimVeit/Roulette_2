using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_06_CompleteState_AmericaTracker : IState
{
    private readonly IGlobalStateMachineProvider _stateMachine;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly ITutorialProgressProvider_Write _tutorialProgressProvider_Write;

    private IEnumerator timerCoroutine;

    public Tutorial_06_CompleteState_AmericaTracker(IGlobalStateMachineProvider stateMachine, DialoguePresenter dialoguePresenter, ITutorialProgressProvider_Write tutorialProgressProvider_Write)
    {
        _stateMachine = stateMachine;
        _dialoguePresenter = dialoguePresenter;
        _tutorialProgressProvider_Write = tutorialProgressProvider_Write;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 06 STATE / AMERICA TRACKER</color>");

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

        _tutorialProgressProvider_Write.CompleteTutorial(6);

        ChangeStateToMain();
    }

    private void ChangeStateToMain()
    {
        _stateMachine.SetState(_stateMachine.GetState<MainState_AmericaTracker>());
    }
}
