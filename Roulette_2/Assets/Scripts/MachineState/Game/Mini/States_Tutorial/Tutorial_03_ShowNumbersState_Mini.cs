using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_03_ShowNumbersState_Mini : IState
{
    private readonly IGlobalStateMachineProvider _stateMachine;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly UIGameSceneRoot_Game _sceneRoot;

    private IEnumerator timerCoroutine;

    public Tutorial_03_ShowNumbersState_Mini(IGlobalStateMachineProvider stateMachine, DialoguePresenter dialoguePresenter, UIGameSceneRoot_Game sceneRoot)
    {
        _stateMachine = stateMachine;
        _dialoguePresenter = dialoguePresenter;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 03 STATE / MINI</color>");

        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);

        timerCoroutine = Timer(3);
        Coroutines.Start(timerCoroutine);


        _dialoguePresenter.Next();
        _sceneRoot.OpenMainPanel();
        _sceneRoot.OpenBalancePanel();
    }

    public void ExitState()
    {
        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);
    }

    private IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ChangeStateTo04();
    }

    private void ChangeStateTo04()
    {
        _stateMachine.SetState(_stateMachine.GetState<Tutorial_04_ShowColorFieldsState_Mini>());
    }
}
