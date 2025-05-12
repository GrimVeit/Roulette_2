using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_Euro : IGlobalStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    private IState _currentState;

    public StateMachine_Euro
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
        states[typeof(CheckTutorialState_Euro)] = new CheckTutorialState_Euro(this, storeGameProgressPresenter);
        states[typeof(Tutorial_01_EuroIntroState_Euro)] = new Tutorial_01_EuroIntroState_Euro(this, dialoguePresenter, betCellActivatorProvider, pseudoChipActivatorProvider);
        states[typeof(Tutorial_02_EuroNumberState_Euro)] = new Tutorial_02_EuroNumberState_Euro(this, dialoguePresenter, sceneRoot, highlightProvider, handPointerProvider);
        states[typeof(Tutorial_03_SplitBetState_Euro)] = new Tutorial_03_SplitBetState_Euro(this, dialoguePresenter, highlightProvider, handPointerProvider);
        states[typeof(Tutorial_04_CornerBetState_Euro)] = new Tutorial_04_CornerBetState_Euro(this, dialoguePresenter, highlightProvider, handPointerProvider);
        states[typeof(Tutorial_05_ColorBetState_Euro)] = new Tutorial_05_ColorBetState_Euro(this, dialoguePresenter, highlightProvider, handPointerProvider);
        states[typeof(Tutorial_06_EvenOddBetState_Euro)] = new Tutorial_06_EvenOddBetState_Euro(this, dialoguePresenter, highlightProvider, handPointerProvider);
        states[typeof(Tutorial_07_RowColumnBetState_Euro)] = new Tutorial_07_RowColumnBetState_Euro(this, dialoguePresenter, highlightProvider, handPointerProvider);
        states[typeof(Tutorial_08_SummaryMonicaState_Euro)] = new Tutorial_08_SummaryMonicaState_Euro(this ,dialoguePresenter, sceneRoot);
        states[typeof(Tutorial_09_NewCharacterIntroState_Euro)] = new Tutorial_09_NewCharacterIntroState_Euro(this, dialoguePresenter);
        states[typeof(Tutorial_10_ChanceExplanationState_Euro)] = new Tutorial_10_ChanceExplanationState_Euro(this, dialoguePresenter, storeGameProgressPresenter, storeGameProgressPresenter);

        states[typeof(MainState_Euro)] = new MainState_Euro(this, sceneRoot, betPresenter, betCellActivatorProvider, pseudoChipActivatorProvider);
        states[typeof(RouletteState_Euro)] = new RouletteState_Euro(this, sceneRoot, roulettePresenter, rouletteBallPresenter, rouletteValueHistoryPresenter, metric_GameCount, metric_GameTypeCount);
        states[typeof(ResultState_Euro)] = new ResultState_Euro(this, sceneRoot, betPresenter, animationFrameProvider, soundProvider);
    }

    public void Initialize()
    {
        SetState(GetState<CheckTutorialState_Euro>());
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
