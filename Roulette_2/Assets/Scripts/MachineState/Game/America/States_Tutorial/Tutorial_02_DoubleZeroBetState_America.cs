using System.Collections;
using UnityEngine;

public class Tutorial_02_DoubleZeroBetState_America : IState
{
    private readonly IGlobalStateMachineProvider _stateMachine;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly IHandPointerProvider _handPointerProvider;
    private readonly IHighlightProvider _highlightProvider;
    private readonly UIGameSceneRoot_Game _sceneRoot;

    private IEnumerator timerCoroutine;

    public Tutorial_02_DoubleZeroBetState_America(IGlobalStateMachineProvider stateMachine, DialoguePresenter dialoguePresenter, IHandPointerProvider handPointerProvider, IHighlightProvider highlightProvider, UIGameSceneRoot_Game sceneRoot)
    {
        _stateMachine = stateMachine;
        _dialoguePresenter = dialoguePresenter;
        _handPointerProvider = handPointerProvider;
        _highlightProvider = highlightProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 02 STATE / AMERICA</color>");

        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);

        timerCoroutine = Timer(3);
        Coroutines.Start(timerCoroutine);

        _sceneRoot.OpenBalancePanel();
        _sceneRoot.OpenMainPanel();

        _dialoguePresenter.Next();
        _handPointerProvider.Activate();
        _handPointerProvider.Move(0);
        _highlightProvider.Select(0);
    }

    public void ExitState()
    {
        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);
    }

    private IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ChangeStateTo02();
    }

    private void ChangeStateTo02()
    {
        _stateMachine.SetState(_stateMachine.GetState<Tutorial_03_ChanceExplanationState_America>());
    }
}
