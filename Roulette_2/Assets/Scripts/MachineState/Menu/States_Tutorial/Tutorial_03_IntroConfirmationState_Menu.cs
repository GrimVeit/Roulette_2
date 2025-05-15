using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_03_IntroConfirmationState_Menu : IState
{
    private readonly IGlobalStateMachineProvider _globalStateMachineProvider;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly UIMainMenuRoot _sceneRoot;

    private IEnumerator coroutineTimer;

    public Tutorial_03_IntroConfirmationState_Menu(IGlobalStateMachineProvider globalStateMachineProvider, DialoguePresenter dialoguePresenter, UIMainMenuRoot sceneRoot)
    {
        _globalStateMachineProvider = globalStateMachineProvider;
        _dialoguePresenter = dialoguePresenter;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 03 STATE / MENU</color>");

        _dialoguePresenter.Next();
        _sceneRoot.OpenPlayerDataPanel();

        if (coroutineTimer != null) Coroutines.Stop(coroutineTimer);

        coroutineTimer = Timer(3);
        Coroutines.Start(coroutineTimer);
    }

    public void ExitState()
    {
        _sceneRoot.ClosePlayerDataPanel();

        if (coroutineTimer != null) Coroutines.Stop(coroutineTimer);
    }

    private IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ChangeStateTo_04();
    }

    private void ChangeStateTo_04()
    {
        _globalStateMachineProvider.SetState(_globalStateMachineProvider.GetState<Tutorial_04_ShowBalanceState_Menu>());
    }
}
