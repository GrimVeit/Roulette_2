using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_08_ClickSpinState_Mini : IState
{
    private readonly IGlobalStateMachineProvider _stateMachine;
    private readonly DialoguePresenter _dialoguePresenter;

    private IEnumerator timerCoroutine;

    public Tutorial_08_ClickSpinState_Mini(IGlobalStateMachineProvider stateMachine, DialoguePresenter dialoguePresenter)
    {
        _stateMachine = stateMachine;
        _dialoguePresenter = dialoguePresenter;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 08 STATE / MINI</color>");

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

        ChangeStateTo09();
    }

    private void ChangeStateTo09()
    {
        _stateMachine.SetState(_stateMachine.GetState<Tutorial_09_RouletteSpinState_Mini>());
    }
}
