using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_08_SummaryMonicaState_Euro : IState
{
    private readonly IGlobalStateMachineProvider _stateMachine;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly UIGameSceneRoot_Game _sceneRoot;

    private IEnumerator timerCoroutine;

    public Tutorial_08_SummaryMonicaState_Euro(IGlobalStateMachineProvider stateMachine, DialoguePresenter dialoguePresenter, UIGameSceneRoot_Game sceneRoot)
    {
        _stateMachine = stateMachine;
        _dialoguePresenter = dialoguePresenter;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 08 STATE / EURO</color>");

        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);

        timerCoroutine = Timer(3);
        Coroutines.Start(timerCoroutine);

        _dialoguePresenter.Next();

        _sceneRoot.CloseBalancePanel();
        _sceneRoot.CloseMainPanel();
    }

    public void ExitState()
    {
        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);
    }

    private IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ChangeStateTo09();
    }

    private void ChangeStateTo09()
    {
        _stateMachine.SetState(_stateMachine.GetState<Tutorial_09_NewCharacterIntroState_Euro>());
    }
}
