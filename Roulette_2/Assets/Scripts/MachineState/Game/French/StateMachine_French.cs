using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_French : IGlobalStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    private IState _currentState;

    public StateMachine_French(UIGameSceneRoot_Game sceneRoot, RouletteBallPresenter rouletteBallPresenter, RoulettePresenter roulettePresenter, RouletteValueHistoryPresenter rouletteValueHistoryPresenter, IMetric_GameCount gameCount, IMetric_GameTypeCount typeCount)
    {
        states[typeof(MainState_French)] = new MainState_French(this, sceneRoot);
        states[typeof(RouletteState_French)] = new RouletteState_French(this, sceneRoot, roulettePresenter, rouletteBallPresenter, rouletteValueHistoryPresenter, gameCount, typeCount);
        states[typeof(ResultState_French)] = new ResultState_French(this, sceneRoot);
    }

    public void Initialize()
    {
        SetState(GetState<MainState_French>());
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
