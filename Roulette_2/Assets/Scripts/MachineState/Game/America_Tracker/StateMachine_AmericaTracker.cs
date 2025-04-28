using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_AmericaTracker : IGlobalStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    private IState _currentState;

    public StateMachine_AmericaTracker(UIGameSceneRoot_Game sceneRoot, RouletteBallPresenter rouletteBallPresenter, RoulettePresenter roulettePresenter, RouletteValueHistoryPresenter rouletteValueHistoryPresenter, IMetric_GameCount gameCount, IMetric_GameTypeCount typeCount)
    {
        states[typeof(MainState_AmericaTracker)] = new MainState_AmericaTracker(this, sceneRoot);
        states[typeof(RouletteState_AmericaTracker)] = new RouletteState_AmericaTracker(this, sceneRoot, roulettePresenter, rouletteBallPresenter, rouletteValueHistoryPresenter, gameCount, typeCount);
        states[typeof(ResultState_AmericaTracker)] = new ResultState_AmericaTracker(this, sceneRoot);
    }

    public void Initialize()
    {
        SetState(GetState<MainState_AmericaTracker>());
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
