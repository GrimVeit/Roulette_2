using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_06_DailyBonus1State_Menu : IState
{
    private readonly IGlobalStateMachineProvider _globalStateMachineProvider;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly UIMainMenuRoot _sceneRoot;
    private readonly IHandPointerProvider _handPointerProvider;

    private IEnumerator coroutineTimer;

    public Tutorial_06_DailyBonus1State_Menu(IGlobalStateMachineProvider globalStateMachineProvider, DialoguePresenter dialoguePresenter, UIMainMenuRoot sceneRoot, IHandPointerProvider handPointerProvider)
    {
        _globalStateMachineProvider = globalStateMachineProvider;
        _dialoguePresenter = dialoguePresenter;
        _sceneRoot = sceneRoot;
        _handPointerProvider = handPointerProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 06 STATE / MENU</color>");

        _dialoguePresenter.Next();
        _handPointerProvider.Move(2);
        _sceneRoot.OpenDailyRewardPanel();

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

        ChangeStateTo_07();
    }

    private void ChangeStateTo_07()
    {
        _globalStateMachineProvider.SetState(_globalStateMachineProvider.GetState<Tutorial_07_DailyBonus2State_Menu>());
    }
}
