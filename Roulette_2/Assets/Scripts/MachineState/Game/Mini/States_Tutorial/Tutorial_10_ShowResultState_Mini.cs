using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_10_ShowResultState_Mini : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameSceneRoot_Game _sceneRoot;
    private readonly BetPresenter _betPresenter;
    private readonly IAnimationFrameProvider _frameProvider;
    private readonly DialoguePresenter _dialoguePresenter;

    private IEnumerator timerCoroutine;

    public Tutorial_10_ShowResultState_Mini(IGlobalStateMachineProvider machineProvider, UIGameSceneRoot_Game sceneRoot, BetPresenter betPresenter, IAnimationFrameProvider frameProvider, DialoguePresenter dialoguePresenter)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _betPresenter = betPresenter;
        _frameProvider = frameProvider;
        _dialoguePresenter = dialoguePresenter;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 08 STATE / MINI</color>");

        _betPresenter.SearchWin();
        _dialoguePresenter.Next();
        _sceneRoot.OpenBalancePanel();
        _sceneRoot.OpenResultPanel();

        _frameProvider.ActivateAnimation("Stars", 1);
        _frameProvider.ActivateAnimation("Confetti", 3);

        if (timerCoroutine != null)
            Coroutines.Stop(timerCoroutine);

        timerCoroutine = Timer(3);
        Coroutines.Start(timerCoroutine);
    }

    public void ExitState()
    {
        _dialoguePresenter.Deactivate();

        if (timerCoroutine != null)
            Coroutines.Stop(timerCoroutine);
    }

    private IEnumerator Timer(int time)
    {
        yield return new WaitForSeconds(time);

        ChangeStateToMain();
    }

    private void ChangeStateToMain()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_Mini>());
    }
}
