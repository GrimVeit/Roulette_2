using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultState_Mini : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameSceneRoot_Game _sceneRoot;
    private readonly BetPresenter _betPresenter;

    private IEnumerator timerCoroutine;

    public ResultState_Mini(IGlobalStateMachineProvider machineProvider, UIGameSceneRoot_Game sceneRoot, BetPresenter betPresenter)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _betPresenter = betPresenter;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - RESULT");

        _betPresenter.SearchWin();
        _sceneRoot.OpenHeaderPanel();
        _sceneRoot.OpenResultPanel();

        if(timerCoroutine != null)
            Coroutines.Stop(timerCoroutine);

        timerCoroutine = Timer(5);
        Coroutines.Start(timerCoroutine);
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - RESULT");

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
