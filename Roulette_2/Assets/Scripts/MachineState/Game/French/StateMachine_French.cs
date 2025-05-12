using System;
using System.Collections.Generic;

public class StateMachine_French : IGlobalStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    private IState _currentState;

    public StateMachine_French
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
        states[typeof(CheckTutorialState_French)] = new CheckTutorialState_French(this, storeGameProgressPresenter);
        states[typeof(Tutorial_01_IntroFrenchState_French)] = new Tutorial_01_IntroFrenchState_French(this, dialoguePresenter, betCellActivatorProvider, pseudoChipActivatorProvider, sceneRoot);
        states[typeof(Tutorial_02_ChampsElyseesState_French)] = new Tutorial_02_ChampsElyseesState_French(this, dialoguePresenter, highlightProvider, handPointerProvider);
        states[typeof(Tutorial_03_OneTwoBetState_French)] = new Tutorial_03_OneTwoBetState_French(this, dialoguePresenter, highlightProvider, handPointerProvider);
        states[typeof(Tutorial_04_LaPartageState_French)] = new Tutorial_04_LaPartageState_French(this, dialoguePresenter, handPointerProvider);
        states[typeof(Tutorial_05_EnPrisonState_French)] = new Tutorial_05_EnPrisonState_French(this, dialoguePresenter, highlightProvider, handPointerProvider);
        states[typeof(Tutorial_06_ChanceExplanationState_French)] = new Tutorial_06_ChanceExplanationState_French(this, dialoguePresenter, storeGameProgressPresenter, storeGameProgressPresenter);

        states[typeof(MainState_French)] = new MainState_French(this, sceneRoot, betPresenter, betCellActivatorProvider, pseudoChipActivatorProvider);
        states[typeof(RouletteState_French)] = new RouletteState_French(this, sceneRoot, roulettePresenter, rouletteBallPresenter, rouletteValueHistoryPresenter, metric_GameCount, metric_GameTypeCount);
        states[typeof(ResultState_French)] = new ResultState_French(this, sceneRoot, betPresenter, animationFrameProvider, soundProvider);
    }

    public void Initialize()
    {
        SetState(GetState<CheckTutorialState_French>());
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
