using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_01_IntroGreetingState_Menu : IState
{
    private readonly IGlobalStateMachineProvider _globalStateMachineProvider;
    private readonly DialoguePresenter _dialoguePresenter;

    private IEnumerator coroutineTimer;

    public Tutorial_01_IntroGreetingState_Menu(IGlobalStateMachineProvider globalStateMachineProvider, DialoguePresenter dialoguePresenter)
    {
        _globalStateMachineProvider = globalStateMachineProvider;
        _dialoguePresenter = dialoguePresenter;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 01 STATE / MENU</color>");

        _dialoguePresenter.Activate();

        if (coroutineTimer != null) Coroutines.Stop(coroutineTimer);

        coroutineTimer = Timer(3);
        Coroutines.Start(coroutineTimer);
    }

    public void ExitState()
    {
        if (coroutineTimer != null) Coroutines.Stop(coroutineTimer);
    }

    private IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ChangeStateTo_02();
    }

    private void ChangeStateTo_02()
    {
        _globalStateMachineProvider.SetState(_globalStateMachineProvider.GetState<Tutorial_02_NameAndAvatarInputState_Menu>());
    }
}
