using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_04_LaPartageState_French : IState
{
    private readonly IGlobalStateMachineProvider _stateMachine;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly IHandPointerProvider _handPointerProvider;

    private IEnumerator timerCoroutine;

    public Tutorial_04_LaPartageState_French(IGlobalStateMachineProvider stateMachine, DialoguePresenter dialoguePresenter, IHandPointerProvider handPointerProvider)
    {
        _stateMachine = stateMachine;
        _dialoguePresenter = dialoguePresenter;
        _handPointerProvider = handPointerProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 04 STATE / FRENCH</color>");

        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);

        timerCoroutine = Timer(4);
        Coroutines.Start(timerCoroutine);


        _dialoguePresenter.Next();
        _handPointerProvider.Move(2);
    }

    public void ExitState()
    {
        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);
    }

    private IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ChangeStateTo05();
    }

    private void ChangeStateTo05()
    {
        _stateMachine.SetState(_stateMachine.GetState<Tutorial_05_EnPrisonState_French>());
    }
}
