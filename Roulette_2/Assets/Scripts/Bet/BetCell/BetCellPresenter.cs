using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetCellPresenter
{
    private readonly BetCellModel _model;
    private readonly BetCellView _view;

    public BetCellPresenter(BetCellModel model, BetCellView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        _view.Initialize();

        ActivatewEvents();
    }

    public void Dispose()
    {
        _view.Dispose();

        DeactivateEvents();
    }

    private void ActivatewEvents()
    {
        _view.OnAddBet += _model.AddBet;
        _view.OnReturnAllBets += _model.ReturnAllBets;
        _view.OnReturnLastBet += _model.ReturnLastBet;
    }

    private void DeactivateEvents()
    {
        _view.OnAddBet -= _model.AddBet;
        _view.OnReturnAllBets -= _model.ReturnAllBets;
        _view.OnReturnLastBet -= _model.ReturnLastBet;
    }
}
