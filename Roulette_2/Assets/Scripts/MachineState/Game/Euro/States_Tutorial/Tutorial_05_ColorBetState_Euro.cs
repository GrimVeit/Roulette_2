using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_05_ColorBetState_Euro : IState
{
    private readonly IGlobalStateMachineProvider _stateMachine;
    private readonly DialoguePresenter _dialoguePresenter;

    private IEnumerator timerCoroutine;

    public Tutorial_05_ColorBetState_Euro(IGlobalStateMachineProvider stateMachine, DialoguePresenter dialoguePresenter)
    {
        _stateMachine = stateMachine;
        _dialoguePresenter = dialoguePresenter;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 05 STATE / EURO</color>");

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

        ChangeStateTo06();
    }

    private void ChangeStateTo06()
    {
        _stateMachine.SetState(_stateMachine.GetState<Tutorial_06_EvenOddBetState_Euro>());
    }
}
