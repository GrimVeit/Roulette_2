using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_02_VoisinsDuZeroState_AmericaTracker : IState
{
    private readonly IGlobalStateMachineProvider _stateMachine;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly IHandPointerProvider _handPointerProvider;

    private IEnumerator timerCoroutine;

    public Tutorial_02_VoisinsDuZeroState_AmericaTracker(IGlobalStateMachineProvider stateMachine, DialoguePresenter dialoguePresenter, IHandPointerProvider handPointerProvider)
    {
        _stateMachine = stateMachine;
        _dialoguePresenter = dialoguePresenter;
        _handPointerProvider = handPointerProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 02 STATE / AMERICA TRACKER</color>");

        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);

        timerCoroutine = Timer(4);
        Coroutines.Start(timerCoroutine);


        _dialoguePresenter.Next();
        _handPointerProvider.Activate();
        _handPointerProvider.Move(0);
    }

    public void ExitState()
    {
        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);
    }

    private IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ChangeStateTo03();
    }

    private void ChangeStateTo03()
    {
        _stateMachine.SetState(_stateMachine.GetState<Tutorial_03_TiersDuCylindreState_AmericaTracker>());
    }
}
