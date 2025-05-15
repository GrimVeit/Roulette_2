using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_10_CompleteState_Menu : IState
{
    private readonly IGlobalStateMachineProvider _globalStateMachineProvider;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly UIMainMenuRoot _sceneRoot;

    private readonly IGameProgressProvider_Write _gameProgressProvider_Write;
    private readonly ITutorialProgressProvider_Write _tutorialProgressProvider_Write;

    private IEnumerator coroutineTimer;

    public Tutorial_10_CompleteState_Menu(IGlobalStateMachineProvider globalStateMachineProvider, DialoguePresenter dialoguePresenter , ITutorialProgressProvider_Write tutorialProgressProvider_Write, UIMainMenuRoot sceneRoot, IGameProgressProvider_Write gameProgressProvider_Write)
    {
        _globalStateMachineProvider = globalStateMachineProvider;
        _dialoguePresenter = dialoguePresenter;
        _tutorialProgressProvider_Write = tutorialProgressProvider_Write;
        _sceneRoot = sceneRoot;
        _gameProgressProvider_Write = gameProgressProvider_Write;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 10 STATE / MENU</color>");

        _dialoguePresenter.Next();
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

        _dialoguePresenter.Deactivate();
        _gameProgressProvider_Write.OpenGame(1);
        _tutorialProgressProvider_Write.CompleteTutorial(0);

        ChangeStateToStartMain();
    }

    private void ChangeStateToStartMain()
    {
        _globalStateMachineProvider.SetState(_globalStateMachineProvider.GetState<StartMainState_Menu>());
    }
}
