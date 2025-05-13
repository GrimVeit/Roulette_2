using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_07_DailyBonus2State_Menu : IState
{
    private readonly IGlobalStateMachineProvider _globalStateMachineProvider;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly IHandPointerProvider _handPointerProvider;

    private IEnumerator coroutineTimer;

    public Tutorial_07_DailyBonus2State_Menu(IGlobalStateMachineProvider globalStateMachineProvider, DialoguePresenter dialoguePresenter, IHandPointerProvider handPointerProvider)
    {
        _globalStateMachineProvider = globalStateMachineProvider;
        _dialoguePresenter = dialoguePresenter;
        _handPointerProvider = handPointerProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 07 STATE / MENU</color>");

        _dialoguePresenter.Next();
        _handPointerProvider.Move(3);

        if (coroutineTimer != null) Coroutines.Stop(coroutineTimer);

        coroutineTimer = Timer(4);
        Coroutines.Start(coroutineTimer);
    }

    public void ExitState()
    {
        if (coroutineTimer != null) Coroutines.Stop(coroutineTimer);
    }

    private IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ChangeStateTo_08();
    }

    private void ChangeStateTo_08()
    {
        _globalStateMachineProvider.SetState(_globalStateMachineProvider.GetState<Tutorial_08_HighlightTasksBtnState_Menu>());
    }
}
