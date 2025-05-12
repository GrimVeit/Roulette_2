using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_Mini : IGlobalStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    private IState _currentState;

    public StateMachine_Mini
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
        states[typeof(CheckTutorialState_Mini)] = new CheckTutorialState_Mini(this, storeGameProgressPresenter);
        states[typeof(Tutorial_01_IntroOliviaState_Mini)] = new Tutorial_01_IntroOliviaState_Mini(this, dialoguePresenter, betCellActivatorProvider, pseudoChipActivatorProvider);
        states[typeof(Tutorial_02_IntroMonicaState_Mini)] = new Tutorial_02_IntroMonicaState_Mini(this, dialoguePresenter);
        states[typeof(Tutorial_03_ShowNumbersState_Mini)] = new Tutorial_03_ShowNumbersState_Mini(this, dialoguePresenter, sceneRoot, highlightProvider, handPointerProvider);
        states[typeof(Tutorial_04_ShowColorFieldsState_Mini)] = new Tutorial_04_ShowColorFieldsState_Mini(this, dialoguePresenter, highlightProvider, handPointerProvider);
        states[typeof(Tutorial_05_ShowChipsState_Mini)] = new Tutorial_05_ShowChipsState_Mini(this, dialoguePresenter, sceneRoot, highlightProvider, handPointerProvider);
        states[typeof(Tutorial_06_ExplainChipPlaceState_Mini)] = new Tutorial_06_ExplainChipPlaceState_Mini(this, dialoguePresenter, highlightProvider);
        states[typeof(Tutorial_07_WaitChipPlaceState_Mini)] = new Tutorial_07_WaitChipPlaceState_Mini(this, betPresenter, pseudoChipActivatorProvider, dialoguePresenter, handPointerProvider);
        states[typeof(Tutorial_08_ClickSpinState_Mini)] = new Tutorial_08_ClickSpinState_Mini(this, dialoguePresenter, sceneRoot, handPointerProvider);
        states[typeof(Tutorial_09_RouletteSpinState_Mini)] = new Tutorial_09_RouletteSpinState_Mini(this, sceneRoot, roulettePresenter, rouletteBallPresenter, rouletteValueHistoryPresenter);
        states[typeof(Tutorial_10_ShowResultState_Mini)] = new Tutorial_10_ShowResultState_Mini(this, sceneRoot, betPresenter, animationFrameProvider, dialoguePresenter, storeGameProgressPresenter, storeGameProgressPresenter, soundProvider);


        states[typeof(MainState_Mini)] = new MainState_Mini(this, sceneRoot, betPresenter, pseudoChipActivatorProvider, betCellActivatorProvider);
        states[typeof(RouletteState_Mini)] = new RouletteState_Mini(this, sceneRoot, roulettePresenter, rouletteBallPresenter, rouletteValueHistoryPresenter, metric_GameCount, metric_GameTypeCount);
        states[typeof(ResultState_Mini)] = new ResultState_Mini(this, sceneRoot, betPresenter, animationFrameProvider, soundProvider);
    }

    public void Initialize()
    {
        SetState(GetState<CheckTutorialState_Mini>());
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
