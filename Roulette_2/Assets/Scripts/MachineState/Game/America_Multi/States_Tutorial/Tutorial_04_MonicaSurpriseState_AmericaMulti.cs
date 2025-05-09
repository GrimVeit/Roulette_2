using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_04_MonicaSurpriseState_AmericaMulti : IState
{
    private readonly IGlobalStateMachineProvider _stateMachine;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly UIGameSceneRoot_Game _sceneRoot;

    private IEnumerator timerCoroutine;

    public Tutorial_04_MonicaSurpriseState_AmericaMulti(IGlobalStateMachineProvider stateMachine, DialoguePresenter dialoguePresenter, UIGameSceneRoot_Game sceneRoot)
    {
        _stateMachine = stateMachine;
        _dialoguePresenter = dialoguePresenter;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 04 STATE / AMERICA MULTI</color>");

        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);

        timerCoroutine = Timer(4);
        Coroutines.Start(timerCoroutine);


        _dialoguePresenter.Next();
        _sceneRoot.OpenRoulettePanel();
    }

    public void ExitState()
    {
        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);
    }

    private IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ChangeStateTo05();
    }

    private void ChangeStateTo05()
    {
        _stateMachine.SetState(_stateMachine.GetState<Tutorial_05_KingState_AmericaMulti>());
    }
}
