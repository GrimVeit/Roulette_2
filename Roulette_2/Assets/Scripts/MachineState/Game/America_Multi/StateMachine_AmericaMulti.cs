using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_AmericaMulti : IGlobalStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    private IState _currentState;

    public StateMachine_AmericaMulti
        (UIGameSceneRoot_Game sceneRoot, 
        List<RouletteBallPresenter> rouletteBallPresenters, 
        List<RoulettePresenter> roulettePresenters, 
        RouletteValueHistoryPresenter rouletteValueHistoryPresenter, 
        BetPresenter betPresenter,
        StoreGameProgressPresenter storeGameProgressPresenter,
        IBetCellActivatorProvider betCellActivatorProvider,
        IPseudoChipActivatorProvider pseudoChipActivatorProvider,
        DialoguePresenter dialoguePresenter,
        IMetric_GameCount gameCount, 
        IMetric_GameTypeCount typeCount,
        IAnimationFrameProvider animationFrameProvider,
        ISoundProvider soundProvider) 
    {
        states[typeof(CheckTutorialState_AmericaMulti)] = new CheckTutorialState_AmericaMulti(this, storeGameProgressPresenter);
        states[typeof(Tutorial_01_MonicaAgainState_AmericaMulti)] = new Tutorial_01_MonicaAgainState_AmericaMulti(this, dialoguePresenter, betCellActivatorProvider, pseudoChipActivatorProvider, sceneRoot);
        states[typeof(Tutorial_02_MaryCatchState_AmericaMulti)] = new Tutorial_02_MaryCatchState_AmericaMulti(this, dialoguePresenter);
        states[typeof(Tutorial_03_MainState_AmericaMulti)] = new Tutorial_03_MainState_AmericaMulti(this, sceneRoot, betPresenter, betCellActivatorProvider, pseudoChipActivatorProvider);
        states[typeof(Tutorial_04_MonicaSurpriseState_AmericaMulti)] = new Tutorial_04_MonicaSurpriseState_AmericaMulti(this, dialoguePresenter, sceneRoot);
        states[typeof(Tutorial_05_KingState_AmericaMulti)] = new Tutorial_05_KingState_AmericaMulti(this, dialoguePresenter);
        states[typeof(Tutorial_06_BetCutState_AmericaMulti)] = new Tutorial_06_BetCutState_AmericaMulti(this, dialoguePresenter);
        states[typeof(Tutorial_07_CompleteState_AmericaMulti)] = new Tutorial_07_CompleteState_AmericaMulti(this, dialoguePresenter, storeGameProgressPresenter, storeGameProgressPresenter);

        states[typeof(MainState_AmericaMulti)] = new MainState_AmericaMulti(this, sceneRoot, betPresenter, betCellActivatorProvider, pseudoChipActivatorProvider);
        states[typeof(RouletteState_AmericaMulti)] = new RouletteState_AmericaMulti(this, sceneRoot, roulettePresenters, rouletteBallPresenters, rouletteValueHistoryPresenter, gameCount, typeCount);
        states[typeof(ResultState_AmericaMulti)] = new ResultState_AmericaMulti(this, sceneRoot, betPresenter, animationFrameProvider, soundProvider);
    }

    public void Initialize()
    {
        SetState(GetState<CheckTutorialState_AmericaMulti>());
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
