using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_America : IGlobalStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    private IState _currentState;

    public StateMachine_America(UIGameSceneRoot_Game sceneRoot, RouletteBallPresenter rouletteBallPresenter, RoulettePresenter roulettePresenter, RouletteValueHistoryPresenter rouletteValueHistoryPresenter, IMetric_GameCount gameCount, IMetric_GameTypeCount typeCount)
    {
        states[typeof(MainState_America)] = new MainState_America(this, sceneRoot);
        states[typeof(RouletteState_America)] = new RouletteState_America(this, sceneRoot, roulettePresenter, rouletteBallPresenter, rouletteValueHistoryPresenter, gameCount, typeCount);
        states[typeof(ResultState_America)] = new ResultState_America(this, sceneRoot);
    }

    public void Initialize()
    {
        SetState(GetState<MainState_America>());
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
