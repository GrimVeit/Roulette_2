using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_03_ShowNumbersState_Mini : IState
{
    private readonly IGlobalStateMachineProvider _stateMachine;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly UIGameSceneRoot_Game _sceneRoot;
    private readonly IHighlightProvider _highlightProvider;
    private readonly IHandPointerProvider _handPointerProvider;

    private IEnumerator timerCoroutine;

    public Tutorial_03_ShowNumbersState_Mini(IGlobalStateMachineProvider stateMachine, DialoguePresenter dialoguePresenter, UIGameSceneRoot_Game sceneRoot, IHighlightProvider highlightProvider, IHandPointerProvider handPointerProvider)
    {
        _stateMachine = stateMachine;
        _dialoguePresenter = dialoguePresenter;
        _sceneRoot = sceneRoot;
        _highlightProvider = highlightProvider;
        _handPointerProvider = handPointerProvider;
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
        _highlightProvider.Select(0);
        _handPointerProvider.Activate();
        _handPointerProvider.Move(0);
    }

    public void ExitState()
    {
        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);

        _highlightProvider.Deselect(0);
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
