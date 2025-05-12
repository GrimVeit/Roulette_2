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
    private readonly IGameProgressProvider_Write _gameProgressProvider_Write;
    private readonly ITutorialProgressProvider_Write _tutorialProgressProvider_Write;
    private readonly ISoundProvider _soundProvider;
    private readonly ISound _soundBackground;

    private IEnumerator timerCoroutine;

    public Tutorial_10_ShowResultState_Mini(IGlobalStateMachineProvider machineProvider, UIGameSceneRoot_Game sceneRoot, BetPresenter betPresenter, IAnimationFrameProvider frameProvider, DialoguePresenter dialoguePresenter, IGameProgressProvider_Write gameProgressProvider_Write, ITutorialProgressProvider_Write tutorialProgressProvider_Write, ISoundProvider soundProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _betPresenter = betPresenter;
        _frameProvider = frameProvider;
        _dialoguePresenter = dialoguePresenter;
        _gameProgressProvider_Write = gameProgressProvider_Write;
        _tutorialProgressProvider_Write = tutorialProgressProvider_Write;
        _soundProvider = soundProvider;
        _soundBackground = _soundProvider.GetSound("Background");
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

        timerCoroutine = Timer();
        Coroutines.Start(timerCoroutine);
    }

    public void ExitState()
    {
        _dialoguePresenter.Deactivate();

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

        _gameProgressProvider_Write.OpenGame(2);
        _tutorialProgressProvider_Write.CompleteTutorial(1);

        ChangeStateToMain();
    }

    private void ChangeStateToMain()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_Mini>());
    }
}
