using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_AmericaTracker : IGlobalStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    private IState _currentState;

    public StateMachine_AmericaTracker
        (UIGameSceneRoot_Game sceneRoot,
        RouletteBallPresenter rouletteBallPresenter,
        RoulettePresenter roulettePresenter,
        RouletteValueHistoryPresenter rouletteValueHistoryPresenter,
        StoreGameProgressPresenter storeGameProgressPresenter,
        BetPresenter betPresenter,
        DialoguePresenter dialoguePresenter,
        IHighlightProvider highlightProvider,
        IHandPointerProvider handPointerProvider,
        IBetCellActivatorProvider betCellActivatorProvider,
        IPseudoChipActivatorProvider pseudoChipActivatorProvider,
        IMetric_GameCount metric_GameCount,
        IMetric_GameTypeCount metric_GameTypeCount,
        IAnimationFrameProvider animationFrameProvider,
        ISoundProvider soundProvider)
    {
        states[typeof(CheckTutorialStates_AmericaTracker)] = new CheckTutorialStates_AmericaTracker(this, storeGameProgressPresenter);
        states[typeof(Tutorial_01_AmericaTrackerIntroState_AmericaTracker)] = new Tutorial_01_AmericaTrackerIntroState_AmericaTracker(this, dialoguePresenter, betCellActivatorProvider, pseudoChipActivatorProvider, sceneRoot);
        states[typeof(Tutorial_02_VoisinsDuZeroState_AmericaTracker)] = new Tutorial_02_VoisinsDuZeroState_AmericaTracker(this, dialoguePresenter, handPointerProvider);
        states[typeof(Tutorial_03_TiersDuCylindreState_AmericaTracker)] = new Tutorial_03_TiersDuCylindreState_AmericaTracker(this, dialoguePresenter, handPointerProvider);
        states[typeof(Tutorial_04_OrphelinsState_AmericaTracker)] = new Tutorial_04_OrphelinsState_AmericaTracker(this, dialoguePresenter, handPointerProvider);
        states[typeof(Tutorial_05_JeuZeroState_AmericaTracker)] = new Tutorial_05_JeuZeroState_AmericaTracker(this, dialoguePresenter, handPointerProvider);
        states[typeof(Tutorial_06_CompleteState_AmericaTracker)] = new Tutorial_06_CompleteState_AmericaTracker(this, dialoguePresenter, storeGameProgressPresenter);

        states[typeof(MainState_AmericaTracker)] = new MainState_AmericaTracker(this, sceneRoot, betPresenter, betCellActivatorProvider, pseudoChipActivatorProvider);
        states[typeof(RouletteState_AmericaTracker)] = new RouletteState_AmericaTracker(this, sceneRoot, roulettePresenter, rouletteBallPresenter, rouletteValueHistoryPresenter, metric_GameCount, metric_GameTypeCount);
        states[typeof(ResultState_AmericaTracker)] = new ResultState_AmericaTracker(this, sceneRoot, betPresenter, animationFrameProvider, soundProvider);
    }

    public void Initialize()
    {
        SetState(GetState<CheckTutorialStates_AmericaTracker>());
    }

    public void Dispose()
    {

    }

    public IState GetState<T>() where T : IState
    {
        return states[typeof(T)];
    }

    public void SetState(IState state)
    {
        _currentState?.ExitState();

        _currentState = state;
        _currentState.EnterState();
    }
}
