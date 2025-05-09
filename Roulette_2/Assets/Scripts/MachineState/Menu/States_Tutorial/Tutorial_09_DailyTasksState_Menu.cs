using System.Collections;
using UnityEngine;

public class Tutorial_09_DailyTasksState_Menu : IState
{
    private readonly IGlobalStateMachineProvider _globalStateMachineProvider;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly UIMainMenuRoot _sceneRoot;

    private IEnumerator coroutineTimer;

    public Tutorial_09_DailyTasksState_Menu(IGlobalStateMachineProvider globalStateMachineProvider, DialoguePresenter dialoguePresenter, UIMainMenuRoot sceneRoot)
    {
        _globalStateMachineProvider = globalStateMachineProvider;
        _dialoguePresenter = dialoguePresenter;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 07 STATE / MENU</color>");

        _dialoguePresenter.Next();
        _sceneRoot.OpenTasksPanel();

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

        ChangeStateTo_10();
    }

    private void ChangeStateTo_10()
    {
        _globalStateMachineProvider.SetState(_globalStateMachineProvider.GetState<Tutorial_10_CompleteState_Menu>());
    }
}
