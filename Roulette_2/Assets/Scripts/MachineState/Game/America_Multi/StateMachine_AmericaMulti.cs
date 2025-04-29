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
        IMetric_GameCount gameCount, 
        IMetric_GameTypeCount typeCount,
        IAnimationFrameProvider animationFrameProvider)
    {
        states[typeof(MainState_AmericaMulti)] = new MainState_AmericaMulti(this, sceneRoot, betPresenter);
        states[typeof(RouletteState_AmericaMulti)] = new RouletteState_AmericaMulti(this, sceneRoot, roulettePresenters, rouletteBallPresenters, rouletteValueHistoryPresenter, gameCount, typeCount);
        states[typeof(ResultState_AmericaMulti)] = new ResultState_AmericaMulti(this, sceneRoot, betPresenter, animationFrameProvider);
    }

    public void Initialize()
    {
        SetState(GetState<MainState_AmericaMulti>());
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
