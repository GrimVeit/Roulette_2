using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_America : IGlobalStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    private IState _currentState;

    public StateMachine_America
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
        states[typeof(CheckTutorialState_America)] = new CheckTutorialState_America(this, storeGameProgressPresenter);
        states[typeof(Tutorial_01_AmericaIntroState_America)] = new Tutorial_01_AmericaIntroState_America(this, dialoguePresenter, betCellActivatorProvider, pseudoChipActivatorProvider);
        states[typeof(Tutorial_02_DoubleZeroBetState_America)] = new Tutorial_02_DoubleZeroBetState_America(this, dialoguePresenter, handPointerProvider, highlightProvider, sceneRoot);
        states[typeof(Tutorial_03_ChanceExplanationState_America)] = new Tutorial_03_ChanceExplanationState_America(this, dialoguePresenter, handPointerProvider, highlightProvider, storeGameProgressPresenter, storeGameProgressPresenter);

        states[typeof(MainState_America)] = new MainState_America(this, sceneRoot, betPresenter, betCellActivatorProvider, pseudoChipActivatorProvider);
        states[typeof(RouletteState_America)] = new RouletteState_America(this, sceneRoot, roulettePresenter, rouletteBallPresenter, rouletteValueHistoryPresenter, metric_GameCount, metric_GameTypeCount);
        states[typeof(ResultState_America)] = new ResultState_America(this, sceneRoot, betPresenter, animationFrameProvider, soundProvider);
    }

    public void Initialize()
    {
        SetState(GetState<CheckTutorialState_America>());
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
