using System.Collections;
using UnityEngine;

public class ResultState_French : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameSceneRoot_Game _sceneRoot;
    private readonly BetPresenter _betPresenter;
    private readonly IAnimationFrameProvider _frameProvider;

    private IEnumerator timerCoroutine;

    public ResultState_French(IGlobalStateMachineProvider machineProvider, UIGameSceneRoot_Game sceneRoot, BetPresenter betPresenter, IAnimationFrameProvider frameProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _betPresenter = betPresenter;
        _frameProvider = frameProvider;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - RESULT");

        _betPresenter.SearchWin();
        _sceneRoot.OpenHeaderPanel();
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
        _machineProvider.SetState(_machineProvider.GetState<MainState_French>());
    }
}
