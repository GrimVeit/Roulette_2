using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_Mini : IGlobalStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    private IState _currentState;

    public StateMachine_Mini(UIGameSceneRoot_Game sceneRoot, RouletteBallPresenter rouletteBallPresenter, RoulettePresenter roulettePresenter, RouletteValueHistoryPresenter rouletteValueHistoryPresenter)
    {
        states[typeof(MainState_Mini)] = new MainState_Mini(this, sceneRoot);
        states[typeof(RouletteState_Mini)] = new RouletteState_Mini(this, sceneRoot, roulettePresenter, rouletteBallPresenter, rouletteValueHistoryPresenter);
        states[typeof(ResultState_Mini)] = new ResultState_Mini(this, sceneRoot);
    }

    public void Initialize()
    {
        SetState(GetState<MainState_Mini>());
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
