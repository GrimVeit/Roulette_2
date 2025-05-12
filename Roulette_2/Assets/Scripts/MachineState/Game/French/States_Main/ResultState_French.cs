using System.Collections;
using UnityEngine;

public class ResultState_French : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameSceneRoot_Game _sceneRoot;
    private readonly BetPresenter _betPresenter;
    private readonly IAnimationFrameProvider _frameProvider;
    private readonly ISoundProvider _soundProvider;
    private readonly ISound _soundBackground;

    private IEnumerator timerCoroutine;

    public ResultState_French(IGlobalStateMachineProvider machineProvider, UIGameSceneRoot_Game sceneRoot, BetPresenter betPresenter, IAnimationFrameProvider frameProvider, ISoundProvider soundProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _betPresenter = betPresenter;
        _frameProvider = frameProvider;
        _soundProvider = soundProvider;
        _soundBackground = _soundProvider.GetSound("Background");
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - RESULT");

        _betPresenter.SearchWin();
        _sceneRoot.OpenBalancePanel();
        _sceneRoot.OpenResultPanel();

        _frameProvider.ActivateAnimation("Stars", 1);
        _frameProvider.ActivateAnimation("Confetti", 3);

        if (timerCoroutine != null)
            Coroutines.Stop(timerCoroutine);

        timerCoroutine = Timer();
        Coroutines.Start(timerCoroutine);
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - RESULT");

        if (timerCoroutine != null)
            Coroutines.Stop(timerCoroutine);
    }

    private IEnumerator Timer()
    {
        _soundProvider.PlayOneShot("Win");
        _soundBackground.SetVolume(0.5f, 0.2f, 0.1f);

        yield return new WaitForSeconds(2);

        _soundBackground.SetVolume(0.2f, 0.5f, 0.1f);

        yield return new WaitForSeconds(1);

        ChangeStateToMain();
    }

    private void ChangeStateToMain()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_French>());
    }
}
