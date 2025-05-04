using System.Collections;
using UnityEngine;

public class Tutorial_02_EuroNumberState_Euro : IState
{
    private readonly IGlobalStateMachineProvider _stateMachine;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly UIGameSceneRoot_Game _sceneRoot;

    private IEnumerator timerCoroutine;

    public Tutorial_02_EuroNumberState_Euro(IGlobalStateMachineProvider stateMachine, DialoguePresenter dialoguePresenter, UIGameSceneRoot_Game sceneRoot)
    {
        _stateMachine = stateMachine;
        _dialoguePresenter = dialoguePresenter;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 02 STATE / EURO</color>");

        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);

        timerCoroutine = Timer(3);
        Coroutines.Start(timerCoroutine);

        _sceneRoot.OpenMainPanel();
        _sceneRoot.OpenBalancePanel();

        _dialoguePresenter.Next();
    }

    public void ExitState()
    {
        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);
    }

    private IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ChangeStateTo03();
    }

    private void ChangeStateTo03()
    {
        _stateMachine.SetState(_stateMachine.GetState<Tutorial_03_SplitBetState_Euro>());
    }
}
