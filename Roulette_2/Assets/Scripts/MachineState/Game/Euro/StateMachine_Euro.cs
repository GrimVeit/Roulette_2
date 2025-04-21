using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_Euro : IGlobalStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    private IState _currentState;

    public StateMachine_Euro(UIGameSceneRoot_Game sceneRoot, RouletteBallPresenter rouletteBallPresenter, RoulettePresenter roulettePresenter)
    {
        states[typeof(MainState_Euro)] = new MainState_Euro(this, sceneRoot);
        states[typeof(RouletteState_Euro)] = new RouletteState_Euro(this, sceneRoot, roulettePresenter, rouletteBallPresenter);
        states[typeof(ResultState_Euro)] = new ResultState_Euro(this, sceneRoot);
    }

    public void Initialize()
    {
        SetState(GetState<MainState_Euro>());
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
