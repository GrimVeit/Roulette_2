using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_01_EuroIntroState_Euro : IState
{
    private readonly IGlobalStateMachineProvider _stateMachine;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly IBetCellActivatorProvider _betCellActivatorProvider;
    private readonly IPseudoChipActivatorProvider _pseudoChipActivatorProvider;

    private IEnumerator timerCoroutine;

    public Tutorial_01_EuroIntroState_Euro(IGlobalStateMachineProvider stateMachine, DialoguePresenter dialoguePresenter, IBetCellActivatorProvider betCellActivatorProvider, IPseudoChipActivatorProvider pseudoChipActivatorProvider)
    {
        _stateMachine = stateMachine;
        _dialoguePresenter = dialoguePresenter;
        _betCellActivatorProvider = betCellActivatorProvider;
        _pseudoChipActivatorProvider = pseudoChipActivatorProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 01 STATE / EURO</color>");

        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);

        timerCoroutine = Timer(3);
        Coroutines.Start(timerCoroutine);


        _dialoguePresenter.Activate();
        _betCellActivatorProvider.Deactivate();
        _pseudoChipActivatorProvider.Deactivate();
    }

    public void ExitState()
    {
        if (timerCoroutine != null) Coroutines.Stop(timerCoroutine);
    }

    private IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ChangeStateTo02();
    }

    private void ChangeStateTo02()
    {
        _stateMachine.SetState(_stateMachine.GetState<Tutorial_02_EuroNumberState_Euro>());
    }
}
