using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_02_MaryCatchState_AmericaMulti : IState
{
    private readonly IGlobalStateMachineProvider _stateMachine;
    private readonly DialoguePresenter _dialoguePresenter;

    private IEnumerator timerCoroutine;

    public Tutorial_02_MaryCatchState_AmericaMulti(IGlobalStateMachineProvider stateMachine, DialoguePresenter dialoguePresenter)
    {
        _stateMachine = stateMachine;
        _dialoguePresenter = dialoguePresenter;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 02 STATE / AMERICA MULTI</color>");

        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);

        timerCoroutine = Timer(2);
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

        ChangeStateTo02();
    }

    private void ChangeStateTo02()
    {
        _stateMachine.SetState(_stateMachine.GetState<Tutorial_03_MainState_AmericaMulti>());
    }
}
