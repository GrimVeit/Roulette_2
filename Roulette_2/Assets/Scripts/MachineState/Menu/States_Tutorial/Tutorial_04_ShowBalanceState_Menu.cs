using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_04_ShowBalanceState_Menu : IState
{
    private readonly IGlobalStateMachineProvider _globalStateMachineProvider;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly UIMainMenuRoot _sceneRoot;
    private readonly IHandPointerProvider _handPointerProvider;

    private IEnumerator coroutineTimer;

    public Tutorial_04_ShowBalanceState_Menu(IGlobalStateMachineProvider globalStateMachineProvider, DialoguePresenter dialoguePresenter, UIMainMenuRoot sceneRoot, IHandPointerProvider handPointerProvider)
    {
        _globalStateMachineProvider = globalStateMachineProvider;
        _dialoguePresenter = dialoguePresenter;
        _sceneRoot = sceneRoot;
        _handPointerProvider = handPointerProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 04 STATE / MENU</color>");

        _dialoguePresenter.Next();

        _handPointerProvider.Activate();
        _handPointerProvider.Move(0);
        _sceneRoot.OpenMainPanel();

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

        ChangeStateTo_05();
    }

    private void ChangeStateTo_05()
    {
        _globalStateMachineProvider.SetState(_globalStateMachineProvider.GetState<Tutorial_05_HighlightBonusBtnState_Menu>());
    }
}
