using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_07_CompleteState_AmericaMulti : IState
{
    private readonly IGlobalStateMachineProvider _stateMachine;
    private readonly DialoguePresenter _dialoguePresenter;

    private IEnumerator timerCoroutine;

    public Tutorial_07_CompleteState_AmericaMulti(IGlobalStateMachineProvider stateMachine, DialoguePresenter dialoguePresenter)
    {
        _stateMachine = stateMachine;
        _dialoguePresenter = dialoguePresenter;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 07 STATE / AMERICA MULTI</color>");

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

        ChangeStateToRoulette();
    }

    private void ChangeStateToRoulette()
    {
        _stateMachine.SetState(_stateMachine.GetState<RouletteState_AmericaMulti>());
    }
}
