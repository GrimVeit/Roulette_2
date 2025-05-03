using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_07_WaitChipPlaceState_Mini : IState
{
    private readonly IGlobalStateMachineProvider _stateMachine;
    private readonly IBetProviderCallBack _betProviderCallBack;
    private readonly IPseudoChipActivatorProvider _pseudoChipActivatorProvider;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly IHandPointerProvider _handPointerProvider;

    public Tutorial_07_WaitChipPlaceState_Mini(IGlobalStateMachineProvider stateMachine, IBetProviderCallBack betProviderCallBack, IPseudoChipActivatorProvider pseudoChipActivatorProvider, DialoguePresenter dialoguePresenter, IHandPointerProvider handPointerProvider)
    {
        _stateMachine = stateMachine;
        _betProviderCallBack = betProviderCallBack;
        _pseudoChipActivatorProvider = pseudoChipActivatorProvider;
        _dialoguePresenter = dialoguePresenter;
        _handPointerProvider = handPointerProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 07 STATE / MINI</color>");

        _betProviderCallBack.OnAddBet += ChangeStateTo08;

        _pseudoChipActivatorProvider.Activate();
        _dialoguePresenter.Deactivate();
        _handPointerProvider.Deactivate();
    }

    public void ExitState()
    {
        _betProviderCallBack.OnAddBet -= ChangeStateTo08;

        _pseudoChipActivatorProvider.Deactivate();
    }

    private void ChangeStateTo08()
    {
        _stateMachine.SetState(_stateMachine.GetState<Tutorial_08_ClickSpinState_Mini>());
    }
}
