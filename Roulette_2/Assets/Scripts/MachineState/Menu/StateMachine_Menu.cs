using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_Menu : IGlobalStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    private IState _currentState;

    public StateMachine_Menu
        (UIMainMenuRoot sceneRoot)
    {
        states[typeof(MainState_Menu)] = new    MainState_Menu(this, sceneRoot);
        states[typeof(DailyRewardState_Menu)] = new DailyRewardState_Menu(this, sceneRoot);
        states[typeof(DailyTasksState_Menu)] = new DailyTasksState_Menu(this, sceneRoot);
        states[typeof(ChipStoreState_Menu)] = new ChipStoreState_Menu(this, sceneRoot);
    }

    public void Initialize()
    {
        SetState(GetState<MainState_Menu>());
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
