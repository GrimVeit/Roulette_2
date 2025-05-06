using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_03_MainState_AmericaMulti : IState
{
    private readonly IGlobalStateMachineProvider _stateProvider;
    private readonly UIGameSceneRoot_Game _sceneRoot;
    private readonly BetPresenter _betPresenter;
    private readonly IBetCellActivatorProvider _betCellActivatorProvider;
    private readonly IPseudoChipActivatorProvider _pseudoChipActivatorProvider;

    public Tutorial_03_MainState_AmericaMulti(IGlobalStateMachineProvider stateProvider, UIGameSceneRoot_Game sceneRoot, BetPresenter betPresenter, IBetCellActivatorProvider betCellActivatorProvider, IPseudoChipActivatorProvider pseudoChipActivatorProvider)
    {
        _sceneRoot = sceneRoot;
        _stateProvider = stateProvider;
        _betPresenter = betPresenter;
        _betCellActivatorProvider = betCellActivatorProvider;
        _pseudoChipActivatorProvider = pseudoChipActivatorProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 03 STATE / AMERICA MULTI</color>");

        _sceneRoot.OnClickToSpin += ChangeStateTo04;

        _sceneRoot.OpenFooterPanel();
        _sceneRoot.OpenBalancePanel();
        _sceneRoot.OpenMainPanel();

        _betPresenter.ClearTable();

        _pseudoChipActivatorProvider.Activate();
        _betCellActivatorProvider.Activate();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToSpin -= ChangeStateTo04;

        _sceneRoot.CloseFooterPanel();
        _sceneRoot.CloseBalancePanel();

        _betCellActivatorProvider.Deactivate();
        _pseudoChipActivatorProvider.Deactivate();
    }

    private void ChangeStateTo04()
    {
        _stateProvider.SetState(_stateProvider.GetState<Tutorial_04_MonicaSurpriseState_AmericaMulti>());
    }
}
